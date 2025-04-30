using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Post.Application.CustomException;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Application.Validation;
using Post.Common.DTOs.Auth;
using Post.Common.Models;
using Post.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Post.Application.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppLogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
                IConfiguration configuration,
            IAppLogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;

        }
        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Register(RegisterDto registerDto)
        {
            _logger.LogInformation("User registration attempt for Email: {Email}", registerDto.Email);


            var validator = new RegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Registration validation failed for Email: {Email}. Errors: {Errors}",
                    registerDto.Email, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new BadRequestException("Invalid Registration Request", validationResult);
            }

            // Check if user already existss
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed. Email already registered: {Email}", registerDto.Email);
                throw new BadRequestException("Email is already registered.");
            }

            // Create new user
            var user = new User
            {
                UserName = registerDto.UserName, 
                Email = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                _logger.LogError("User registration failed for Email: {Email}. Errors: {Errors}");
                throw new BadRequestException("User registration failed");
            }

            _logger.LogInformation("User successfully registered with Email: {Email}", registerDto.Email);
            return true;
        }

        public async Task<TokenResponseDto> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Login attampt for Email: {Email}", loginDto.Email);

            var validator = new LoginDtoValidator();
            var validation = await validator.ValidateAsync(loginDto);

            if (!validation.IsValid)
            {
                var errors = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
                _logger.LogWarning("Login validation failed for {Email}: {Errors}", loginDto.Email, errors);
                throw new BadRequestException("Invalid login", validation);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed. No user for Email: {Email}", loginDto.Email);
                throw new BadRequestException("Invalid credentials");
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                _logger.LogWarning("Login failed. Wrong password for {Email}", loginDto.Email);
                throw new BadRequestException("Invalid credentials");
            }

            _logger.LogInformation("Password validated for {UserName}", user.UserName);

            // build claims
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,           user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // create the signing credentials
            var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);
            var signingKey = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // assemble the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            // create and write the token
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(tokenDescriptor);
            var tokenString = handler.WriteToken(securityToken);

            _logger.LogInformation("Generated JWT for {UserName}", user.UserName);

            return new TokenResponseDto
            {
                AccessToken = tokenString,
                ExpiresAt = securityToken.ValidTo
            };
        }

    }
}

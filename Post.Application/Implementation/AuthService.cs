using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Post.Application.CustomException;
using Post.Application.Interfaces;
using Post.Application.Logging;
using Post.Application.Validation;
using Post.Common.DTOs.Auth;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppLogger<AuthService> _logger;
        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,

            IAppLogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

        }
        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Register(RegisterDto registerDto)
        {
            _logger.LogInformation("User registration attempt for Email: {Email}", registerDto.Email);

            // Validate request
            var validator = new RegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Registration validation failed for Email: {Email}. Errors: {Errors}",
                    registerDto.Email, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new BadRequestException("Invalid Registration Request", validationResult);
            }

            // Check if user already exists
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
        public async Task<bool> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt for Email: {Email}", loginDto.Email);

            var validator = new LoginDtoValidator();
            var validationResult = await validator.ValidateAsync(loginDto);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Login validation failed for Email: {Email}. Errors: {Errors}",
                    loginDto.Email, string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

                throw new BadRequestException("Invalid Login Request", validationResult);
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                _logger.LogWarning("Login failed. User not found for Email: {Email}", loginDto.Email);
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, loginDto.Password, loginDto.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed for User: {UserName}", user.UserName);
                return false;
            }

            _logger.LogInformation("Login successful for User: {UserName}", user.UserName);

            return true;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Interfaces;
using Post.Common.DTOs.Auth;
using Post.Common.Models;

namespace Post.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (!result)
                return BadRequest(ApiResponse<object>.ErrorResponse("User registration failed"));

                return Ok(ApiResponse<object>.SuccessResponse(null, "User registered successfully"));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var result = await _authService.Login(login);
            if (!result)
                return BadRequest(ApiResponse<object>.ErrorResponse("User Login failed"));

            return Ok(ApiResponse<object>.SuccessResponse(null, "User Login successfully"));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok(ApiResponse<object>.SuccessResponse(null, "Logged out successfully"));
        }
    }
}

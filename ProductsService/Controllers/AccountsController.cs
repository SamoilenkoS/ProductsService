using Microsoft.AspNetCore.Mvc;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProductsBusinessLayer.Services.AuthService;
using ProductsBusinessLayer.Services.UserService;
using ProductsCore.Requests;
using Microsoft.Extensions.Logging;

namespace ProductsPresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IAuthService authService,
            IUserService userService,
            ILogger<AccountsController> logger)
        {
            _authService = authService;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("manager")]
        public async Task<IActionResult> CreateManager(AccountInfo accountInfo)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPut("mark_obsolete")]
        public async Task<IActionResult> MarkAccountsPasswordObsolete(List<Guid> accountsIds)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(PasswordChangeRequest request)
        {
            var authHeader = Request.Headers["Authorization"][0];

            var userInfo = _authService.GetUserInfoFromToken(authHeader);

            var loginInfo = new LoginInfo
            {
                Login = userInfo.Login,
                Password = request.OldPassword
            };

            var passwordCorrect = await _userService.VerifyPasswordAsync(loginInfo);
            if (passwordCorrect)
            {
                loginInfo.Password = request.NewPassword;
                await _userService.UpdatePasswordAsync(loginInfo);

                return Ok("Password updated!");
            }

            return BadRequest("Invalid login or password!");
        }

        [HttpPut("info")]
        public async Task<IActionResult> UpdateInfo(AccountInfo accountInfo)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            var userRole = await _userService.GetRoleByLoginInfoAsync(loginInfo);
            if(userRole != null)
            {
                var token = _authService.CreateAuthToken(new UserInfo
                {
                    Login = loginInfo.Login,
                    Role = userRole.Value
                });
                _logger.LogInformation($"User logged in: {loginInfo.Login}");

                return Ok(token);
            }

            _logger.LogWarning($"User login failed: {loginInfo}");
            return BadRequest("Invalid username or password.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountInfo accountInfo)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}

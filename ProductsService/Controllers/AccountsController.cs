﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsBusinessLayer;

namespace ProductsPresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountsController(IAuthService authService)
        {
            _authService = authService;
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

        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(string oldPassword, string newPassword)
        {
            await Task.CompletedTask;
            return Ok();
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
            return Ok(_authService.Login(loginInfo));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountInfo accountInfo)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}

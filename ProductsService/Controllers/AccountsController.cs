using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsPresentationLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
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
        public async Task<IActionResult> Login(LoginInfo login)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountInfo accountInfo)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}

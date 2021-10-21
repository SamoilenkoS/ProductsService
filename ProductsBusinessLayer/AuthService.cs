using System;
using System.Collections.Generic;
using System.Text;
using ProductsCore.Options;
using Microsoft.Extensions.Options;
using ProductsCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ProductsDataLayer;
using System.Threading.Tasks;

namespace ProductsBusinessLayer
{
    public class AuthService : IAuthService
    {
        private readonly AuthOptions _authOptions;
        private readonly IUserRepository _userRepository;

        public AuthService(
            IOptions<AuthOptions> authOptions,
            IUserRepository userRepository)
        {
            _authOptions = authOptions.Value;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginInfo loginInfo)
        {
            var role = await _userRepository.GetRoleByLoginInfoAsync(loginInfo);
            if (role == null)
            {
                return string.Empty;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, loginInfo.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
            };

            var token = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.TokenLifetime)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                            (Encoding.ASCII.GetBytes(_authOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

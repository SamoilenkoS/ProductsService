using System;
using System.Collections.Generic;
using System.Text;
using ProductsCore.Options;
using Microsoft.Extensions.Options;
using ProductsCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace ProductsBusinessLayer.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AuthOptions _authOptions;
        private readonly SigningCredentials _signingCredentials;

        public AuthService(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey
                            (Encoding.ASCII.GetBytes(_authOptions.SecretKey)),
                            SecurityAlgorithms.HmacSha256);
        }

        public string CreateAuthToken(UserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userInfo.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userInfo.Role.ToString())
            };

            var token = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.TokenLifetime)),
                    signingCredentials: _signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserInfo GetUserInfoFromToken(string headerToken)
        {
            var token = headerToken.Substring(headerToken.IndexOf(' ') + 1);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            return new UserInfo
            {
                Login = tokenS.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value,
                Role = (Role)Enum.Parse(typeof(Role), tokenS.Claims.First(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value)
            };
        }
    }
}

using ProductsCore.Models;

namespace ProductsBusinessLayer.Services.AuthService
{
    public interface IAuthService
    {
        string CreateAuthToken(UserInfo userInfo);

        UserInfo GetUserInfoFromToken(string headerToken);
    }
}
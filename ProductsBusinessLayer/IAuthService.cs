using ProductsCore.Models;

namespace ProductsBusinessLayer
{
    public interface IAuthService
    {
        string Login(LoginInfo loginInfo);
    }
}
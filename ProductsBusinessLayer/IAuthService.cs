using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginInfo loginInfo);
    }
}
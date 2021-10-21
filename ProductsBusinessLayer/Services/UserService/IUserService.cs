using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.UserService
{
    public interface IUserService
    {
        Task UpdatePasswordAsync(LoginInfo loginInfo);
        Task<bool> VerifyPasswordAsync(LoginInfo loginInfo);
        Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo);
    }
}

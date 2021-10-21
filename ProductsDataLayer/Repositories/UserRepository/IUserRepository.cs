using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsDataLayer.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo);
        Task UpdatePasswordAsync(LoginInfo loginInfo);
        Task<bool> VerifyLoginInfoAsync(LoginInfo loginInfo);
    }
}

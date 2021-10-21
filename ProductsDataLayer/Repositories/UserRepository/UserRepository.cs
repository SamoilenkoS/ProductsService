using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System.Threading.Tasks;

namespace ProductsDataLayer.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly EFCoreContext _dbContext;

        public UserRepository(EFCoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo)
        {
            var account = await GetAccountInfoByLoginInfoAsync(loginInfo);

            return account?.Role;
        }

        public async Task UpdatePasswordAsync(LoginInfo loginInfo)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.LoginInfo.Login == loginInfo.Login);
            user.LoginInfo.Password = loginInfo.Password;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyLoginInfoAsync(LoginInfo loginInfo)
        {
            var account = await GetAccountInfoByLoginInfoAsync(loginInfo);

            return account != null;
        }

        private async Task<AccountInfo> GetAccountInfoByLoginInfoAsync(LoginInfo loginInfo)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(accountInfo =>
                            accountInfo.LoginInfo.Login == loginInfo.Login &&
                            accountInfo.LoginInfo.Password == loginInfo.Password);
        }
    }
}

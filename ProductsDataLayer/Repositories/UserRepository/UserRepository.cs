using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System;
using System.Linq;
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

        public async Task<Guid> AddUserAsync(AccountInfo accountInfo)
        {
            accountInfo.Id = Guid.NewGuid();
            _dbContext.Users.Add(accountInfo);
            var result = await _dbContext.SaveChangesAsync();

            return result != 0 ? accountInfo.Id : Guid.Empty;
        }

        public async Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo)
        {
            var account = await GetAccountInfoByLoginInfoAsync(loginInfo);

            return account?.Role;
        }

        public async Task<AccountInfo> GetAccountInfoByLoginInfo(LoginInfo loginInfo)
        {
            var getFullAccountInfoQuery = from email in _dbContext.Set<Email>().Cast<Email>()
                        join account in _dbContext.Set<AccountInfo>().Cast<AccountInfo>()
                            on email.Id equals account.EmailId
                        where account.LoginInfo.Login == loginInfo.Login &&
                        account.LoginInfo.Password == loginInfo.Password
                        select new { Email = email, AccountInfo = account };

            var emailAndAccountInfos = await getFullAccountInfoQuery.ToListAsync();

            var emailAndAccountInfo = emailAndAccountInfos.FirstOrDefault();
            if(emailAndAccountInfo != null)
            {
                emailAndAccountInfo.AccountInfo.Email = emailAndAccountInfo.Email;
                return emailAndAccountInfo.AccountInfo;
            }

            return null;
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

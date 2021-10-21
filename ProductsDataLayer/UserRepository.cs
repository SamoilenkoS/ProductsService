using Microsoft.EntityFrameworkCore;
using ProductsCore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsDataLayer
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
            var account = await _dbContext.Users.Where(accountInfo =>
                accountInfo.LoginInfo.Login == loginInfo.Login &&
                accountInfo.LoginInfo.Password == loginInfo.Password)
                .FirstOrDefaultAsync();

            return account?.Role;
        }
    }
}

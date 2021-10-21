using ProductsCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDataLayer
{
    public interface IUserRepository
    {
        Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo);
    }
}

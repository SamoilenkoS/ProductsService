using ProductsCore.Models;
using ProductsDataLayer.Repositories.UserRepository;
using System;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Role?> GetRoleByLoginInfoAsync(LoginInfo loginInfo)
        {
            return await _userRepository.GetRoleByLoginInfoAsync(loginInfo);
        }

        public async Task UpdatePasswordAsync(LoginInfo loginInfo)
        {
            await _userRepository.UpdatePasswordAsync(loginInfo);
        }

        public async Task<bool> VerifyPasswordAsync(LoginInfo loginInfo)
        {
            return await _userRepository.VerifyLoginInfoAsync(loginInfo);
        }
    }
}

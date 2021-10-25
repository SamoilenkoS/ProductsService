using ProductsCore.Models;

namespace ProductsBusinessLayer.DTOs
{
    public class AccountInfoDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LoginInfo LoginInfo { get; set; }
        public string Email { get; set; }
    }
}

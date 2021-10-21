using Microsoft.EntityFrameworkCore;

namespace ProductsCore.Models
{
    [Owned]
    public class LoginInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
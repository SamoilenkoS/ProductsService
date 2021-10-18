using System;

namespace ProductsCore.Models
{
    public class AccountInfo
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public AccountType AccountType { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCore.Models
{
    public class AccountInfo
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public LoginInfo LoginInfo { get; set; }
        [ForeignKey("Email")]
        public int? EmailId { get; set; }
        public Email Email { get; set; }
        [Column("RoleId")]
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }
}

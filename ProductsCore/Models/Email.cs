using System.ComponentModel.DataAnnotations;

namespace ProductsCore.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public string PostName { get; set; }
        public string ConfirmationString { get; set; }
        public bool IsConfirmed { get; set; }
    }
}

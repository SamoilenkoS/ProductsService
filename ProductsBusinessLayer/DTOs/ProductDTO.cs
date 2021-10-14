using System.ComponentModel.DataAnnotations;

namespace ProductsBusinessLayer.DTOs
{
    public class ProductDTO
    {
        [Required]
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}

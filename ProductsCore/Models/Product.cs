using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCore.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [Column("CategoryId")]
        public Category Category { get; set; }
        public bool IsAvailableToBuy { get; set; }
        //TODO Add Sizes
    }
}

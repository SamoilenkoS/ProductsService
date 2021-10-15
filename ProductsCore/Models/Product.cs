using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsCore.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        [Column("CategoryId")]
        [JsonIgnore]
        public Category Category { get; set; }
        public string CategoryTitle { get => Category.ToString(); }
        public bool IsAvailableToBuy { get; set; }
        //TODO Add Sizes
    }
}

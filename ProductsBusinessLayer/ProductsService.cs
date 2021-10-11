using ProductsBusinessLayer.DTOs;
using ProductsCore.Models;
using ProductsDataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsBusinessLayer
{
    public class ProductsService : IProductsService
    {
        public ProductsService()
        {
        }

        private static ProductsRepository _productsRepository;

        static ProductsService()
        {
            _productsRepository = new ProductsRepository();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            await Task.CompletedTask;

            return _productsRepository.GetAll();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            await Task.CompletedTask;

            return _productsRepository.GetById(id);
        }

        public async Task<Product> DeleteProductById(Guid id)
        {
            await Task.CompletedTask;

            return _productsRepository.DeleteById(id);
        }

        public async Task<Guid> CreateProduct(ProductDTO productDTO)
        {
            await Task.CompletedTask;
            if (Enum.TryParse(typeof(Category), productDTO.Category, out var category))
            {
                var product = new Product
                {
                    Category = (Category)category,
                    IsAvailableToBuy = true,
                    Price = productDTO.Price,
                    Title = productDTO.Title
                };

                return _productsRepository.Create(product);
            }

            return Guid.Empty;
        }

        public async Task<Product> UpdateProduct(Guid id, ProductDTO productDTO)
        {
            await Task.CompletedTask;
            if (Enum.TryParse(typeof(Category), productDTO.Category, out var category))
            {
                var product = new Product
                {
                    Id = id,
                    Category = (Category)category,
                    Price = productDTO.Price,
                    Title = productDTO.Title
                };

                return _productsRepository.Update(product);
            }

            return null;
        }
    }
}

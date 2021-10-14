using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsService(IMapper mapper)
        {
            _mapper = mapper;
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
            var product = _mapper.Map<Product>(productDTO);

            return _productsRepository.Create(product);
        }

        public async Task<Product> UpdateProduct(Guid id, ProductDTO productDTO)
        {
            await Task.CompletedTask;
            var product = _mapper.Map<Product>(productDTO);
            product.Id = id;

            return _productsRepository.Update(product);
        }
    }
}

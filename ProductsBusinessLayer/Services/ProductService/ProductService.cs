using AutoMapper;
using ProductsBusinessLayer.DTOs;
using ProductsCore.Models;
using ProductsDataLayer.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productsRepository;

        public ProductService(IProductRepository productsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productsRepository.GetAll();
        }

        public async Task<Product> GetProductById(Guid id)
            => await _productsRepository.GetById(id);

        public async Task<Product> DeleteProductById(Guid id)
            => await _productsRepository.DeleteById(id);

        public async Task<Guid> CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);

            return await _productsRepository.Create(product);
        }

        public async Task<Product> UpdateProduct(Guid id, ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            product.Id = id;

            return await _productsRepository.Update(product);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProductsBusinessLayer;
using ProductsBusinessLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace ProductsPresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static ProductsService _productsService;

        static ProductsController()
        {
            _productsService = new ProductsService();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var items = await _productsService.GetAllProducts();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var item = await _productsService.GetProductById(id);

            if(item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            var item = await _productsService.DeleteProductById(id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            var guid = await _productsService.CreateProduct(product);

            if (guid != Guid.Empty)
            {
                return Ok(guid);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDTO product)
        {
            var updatedProduct = await _productsService.UpdateProduct(id, product);

            if (updatedProduct != null)
            {
                return Ok(updatedProduct);
            }

            return BadRequest();
        }
    }
}

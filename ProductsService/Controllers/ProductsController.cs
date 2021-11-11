using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsBusinessLayer.DTOs;
using ProductsBusinessLayer.Services.ProductService;
using System;
using System.Threading.Tasks;

namespace ProductsPresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static IProductService _productsService;
        private ILogger<ProductsController> _loggger;

        public ProductsController(
            IProductService productsService,
            ILogger<ProductsController> loggger)
        {
            _productsService = productsService;
            _loggger = loggger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var items = await _productsService.GetAllProducts();

                return Ok(items);
            }
            catch(Exception ex)
            {
                _loggger.LogError(ex.Message);

                return StatusCode(500);
            }
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

        [HttpGet("query")]
        public async Task<IActionResult> SearchProduct(string query)
        {
            await Task.CompletedTask;
            return Ok();
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

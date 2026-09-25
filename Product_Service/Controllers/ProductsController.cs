using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var createdProduct =
                await _productService.CreateProductAsync(product);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = createdProduct.ProductId },
                createdProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products =
                await _productService.GetAllProductsAsync();

            return Ok(products);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product =
                await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} was not found."
                });
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            Product product)
        {
            var updatedProduct =
                await _productService.UpdateProductAsync(id, product);

            if (updatedProduct == null)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} was not found."
                });
            }

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted =
                await _productService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} was not found."
                });
            }

            return NoContent();
        }


        [HttpGet("category")]
        public async Task<IActionResult> GetProductsByCategory(
           [FromQuery] string category)
        {
            var products =
                await _productService.GetProductsByCategoryAsync(category);

            return Ok(products);
        }

        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(
            int id,
            [FromBody] int quantity)
        {
            if (quantity < 0)
            {
                return BadRequest(new
                {
                    message = "Available quantity cannot be negative."
                });
            }

            var updatedProduct =
                await _productService.UpdateStockAsync(id, quantity);

            if (updatedProduct == null)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} was not found."
                });
            }

            return Ok(updatedProduct);
        }
    }
}
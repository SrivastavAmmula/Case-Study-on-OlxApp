using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxAppApi.Entities;
using OlxAppApi.Repository;

namespace OlxAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet, Route("GetAllProducts")]
/*        [Authorize(Roles = "Admin")]
*/        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet, Route("ProductId")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet, Route("ProductbyCategoryId")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryId(string categoryId)
        {
            try
            {
                var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
                if (products == null || !products.Any())
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost, Route("AddProduct")]
        public async Task<ActionResult> AddProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product object is null");
                }

                await _productRepository.AddProductAsync(product);
                return Ok (product);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut, Route("EditProduct")]
        public async Task<ActionResult> UpdateProduct(string id, Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product object is null");
                }

                if (id != product.ProductId)
                {
                    return BadRequest("Product ID mismatch");
                }

                await _productRepository.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete, Route("DeleteProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _productRepository.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<Product>>> GetFilteredProducts(
            [FromQuery] string location = null,
            [FromQuery] string name = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null)
        {
            try
            {
                var products = await _productRepository.GetFilteredProductsAsync(location, name, minPrice, maxPrice);
                if (!products.Any())
                {
                    return NotFound("No products found matching the specified criteria.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}

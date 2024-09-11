using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxAppApi.Entities;
using OlxAppApi.Repository;

namespace OlxAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;   
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet, Route("AllCategories")]
        //[Authorize(Roles = "Admin,User")]
        //[AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet, Route("CategoryById")]
        public async Task<ActionResult<Category>> GetCategoryById(string id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost, Route("AddCategory")]
        public async Task<ActionResult> AddCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category object is null");
                }

                await _categoryRepository.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut, Route("EditCategory")]
        public async Task<ActionResult> UpdateCategory(string id, Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Category object is null");
                }

                if (id != category.CategoryId)
                {
                    return BadRequest("Category ID mismatch");
                }

                await _categoryRepository.UpdateCategoryAsync(category);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete, Route("DeleteCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                await _categoryRepository.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}

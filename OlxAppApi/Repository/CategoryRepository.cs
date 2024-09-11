using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in production)
                Console.WriteLine($"Error in AddCategoryAsync: {ex.Message}");
                throw; // Re-throw the exception to let it propagate
            }
        }
        public async Task DeleteCategoryAsync(string id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteCategoryAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllCategoriesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
        {
            try
            {
                return await _context.Categories.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetCategoryByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateCategoryAsync: {ex.Message}");
                throw;
            }
        }
    }
}

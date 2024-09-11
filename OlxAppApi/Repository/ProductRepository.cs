using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework in production)
                Console.WriteLine($"Error in AddProductAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProductAsync(string id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteProductAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products
                                     .Include(p => p.User)
                                     .Include(p => p.Category)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> GetFilteredProductsAsync(string location = null, string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            try
            {
                IQueryable<Product> query = _context.Products.Include(p => p.User).Include(p => p.Category);

                if (!string.IsNullOrEmpty(location))
                {
                    query = query.Where(p => p.Location == location);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(p => p.ProductName.Contains(name));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetFilteredProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            try
            {
                return await _context.Products
                                     .Include(p => p.User)
                                     .Include(p => p.Category)
                                     .FirstOrDefaultAsync(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetProductByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(string categoryId)
        {
            try
            {
                return await _context.Products
                                     .Include(p => p.User)
                                     .Include(p => p.Category)
                                     .Where(p => p.CategoryId == categoryId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetProductsByCategoryIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateProductAsync: {ex.Message}");
                throw;
            }
        }
    }
}

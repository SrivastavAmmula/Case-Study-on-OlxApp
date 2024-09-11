using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in production)
                Console.WriteLine($"Error in AddOrderAsync: {ex.Message}");
                throw; // Re-throw the exception to let it propagate
            }
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteOrderAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllOrdersAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.OrderId == id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetOrderByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.User)
                    .Where(o => o.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetOrdersByUserIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateOrderAsync: {ex.Message}");
                throw;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"Error in AddUserAsync: {ex.Message}");
                throw; // Re-throw the exception to let it propagate
            }
        }
        public async Task<User> ValidUser(string email, string password)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(u => u.UserEmail == email && u.Password == password); // Retrieves user by email and password
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception("An error occured while validating the user.", ex);
            }

        }

        public async Task DeleteUserAsync(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteUserAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllUsersAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == email);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetUserByEmailAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateUserAsync: {ex.Message}");
                throw;
            }
        }


    }
}

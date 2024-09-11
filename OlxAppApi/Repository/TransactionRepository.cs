using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction transaction)
        {
            try
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (in production, use a logging framework)
                Console.WriteLine($"Error in AddAsync: {ex.Message}");
                throw; // Re-throw the exception to let it propagate
            }
        }

        public async Task DeleteAsync(Guid transactionId)
        {
            try
            {
                var transaction = await _context.Transactions.FindAsync(transactionId);
                if (transaction != null)
                {
                    _context.Transactions.Remove(transaction);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            try
            {
                return await _context.Transactions.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Transaction> GetByIdAsync(Guid transactionId)
        {
            try
            {
                return await _context.Transactions.FindAsync(transactionId);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Transaction>> GetByUserIdAsync(string userId)
        {
           try
            {
                return await _context.Transactions
                    .Where(t => t.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetByUserIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            try
            {
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateAsync: {ex.Message}");
                throw;
            }
        }
    }
}
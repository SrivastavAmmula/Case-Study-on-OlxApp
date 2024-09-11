using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
        Task<Transaction> GetByIdAsync(Guid transactionId);
        Task<List<Transaction>> GetAllAsync();
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(Guid transactionId);
        Task<List<Transaction>> GetByUserIdAsync(string userId);
    }
}

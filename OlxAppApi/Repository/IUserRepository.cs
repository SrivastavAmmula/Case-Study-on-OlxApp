using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
        Task<User> ValidUser(string email, string password);
        

    }
}

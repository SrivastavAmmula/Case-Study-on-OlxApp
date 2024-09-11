using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAddressAsync(Address address)
        {
            try
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in production)
                Console.WriteLine($"Error in AddAddressAsync: {ex.Message}");
                throw; // Re-throw the exception to let it propagate
            }
        }
        public async Task DeleteAddressAsync(string id)
        {
            try
            {
                var address = await _context.Addresses.FindAsync(id);
                if (address != null)
                {
                    _context.Addresses.Remove(address);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in DeleteAddressAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Address> GetAddressByIdAsync(string id)
        {
            try
            {
                return await _context.Addresses.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAddressByIdAsync: {ex.Message}");
                throw;
            }

        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            try
            {
                return await _context.Addresses.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllAddressesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAddressAsync(Address address)
        {
            try
            {
                _context.Addresses.Update(address);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in UpdateAddressAsync: {ex.Message}");
                throw;
            }
        }
    }
}

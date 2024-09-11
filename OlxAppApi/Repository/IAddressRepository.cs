using OlxAppApi.Entities;

namespace OlxAppApi.Repository
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<Address> GetAddressByIdAsync(string id);
        Task AddAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(string id);
    }
}

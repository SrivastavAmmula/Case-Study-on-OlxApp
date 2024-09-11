using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxAppApi.Entities;
using OlxAppApi.Repository;

namespace OlxAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet, Route("AllAddress")]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            try
            {
                var addresses = await _addressRepository.GetAllAddressesAsync();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet, Route("AddressById")]
        public async Task<ActionResult<Address>> GetAddressById(string id)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(id);
                if (address == null)
                {
                    return NotFound();
                }
                return Ok(address);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost, Route("AddAddress")]
        public async Task<ActionResult> AddAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest("Address object is null");
                }

                await _addressRepository.AddAddressAsync(address);
                return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressId }, address);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut, Route("EditAddress")]
        public async Task<ActionResult> UpdateAddress(string id, Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest("Address object is null");
                }

                if (id != address.AddressId)
                {
                    return BadRequest("Address ID mismatch");
                }

                await _addressRepository.UpdateAddressAsync(address);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete, Route("DeleteAddress")]
        public async Task<ActionResult> DeleteAddress(string id)
        {
            try
            {
                var address = await _addressRepository.GetAddressByIdAsync(id);
                if (address == null)
                {
                    return NotFound();
                }

                await _addressRepository.DeleteAddressAsync(id);
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

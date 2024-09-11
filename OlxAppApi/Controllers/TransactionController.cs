using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlxAppApi.Entities;
using OlxAppApi.Repository;

namespace OlxAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: api/Transaction/GetAllTransactions
        [HttpGet, Route("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Transaction/GetById/{id}
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            try
            {
                var transaction = await _transactionRepository.GetByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Transaction/AddTransaction
        [HttpPost, Route("AddTransaction")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest("Transaction object is null");
                }

                await _transactionRepository.AddAsync(transaction);
                return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.TransactionId }, transaction);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Transaction/UpdateTransaction/{id}
        [HttpPut("UpdateTransaction/{id}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] Transaction transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest("Transaction object is null");
                }

                if (id != transaction.TransactionId)
                {
                    return BadRequest("Transaction ID mismatch");
                }

                await _transactionRepository.UpdateAsync(transaction);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Transaction/DeleteTransaction/{id}
        [HttpDelete, Route("DeleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {
                var transaction = await _transactionRepository.GetByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                await _transactionRepository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Transaction/GetTransactionsByUserId/{userId}
        [HttpGet("GetTransactionsByUserId/{userId}")]
        public async Task<IActionResult> GetTransactionsByUserId(string userId)
        {
            try
            {
                var transactions = await _transactionRepository.GetByUserIdAsync(userId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }

}
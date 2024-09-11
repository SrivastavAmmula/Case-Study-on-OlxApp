using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OlxAppApi.Entities;
using OlxAppApi.Model;
using OlxAppApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OlxAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpGet,Route("GetAllUser")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost, Route("Login")]
        //[AllowAnonymous]
        public async Task<IActionResult> ValidUser([FromBody] Login login)
        {
            try
            {
                AuthReponse authoresforusers = null;
                var user = await _userRepository.ValidUser(login.Email, login.Password);
                if (user != null)
                {
                    authoresforusers = new AuthReponse
                    {
                        UserId = user.UserId,
                        Role = user.Role,
                        Token = GetToken(user), // Generate JWT token
                    };
                return Ok(authoresforusers);
                }
                else
                {
                    return NotFound("user not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception($"Internal server error: {ex.Message}");
            }

        }

        [HttpGet, Route("GetUserById")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet, Route("GetUserEmail")]
        
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost, Route("AddUser")]
        public async Task<ActionResult> AddUser(User user)
        {
            try
            {
                await _userRepository.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut, Route("UpdateUser")]
        
        public async Task<ActionResult> UpdateUser(string id, User user)
        {
            try
            {
                if (id != user.UserId)
                    return BadRequest("User ID mismatch");

                await _userRepository.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete, Route("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    return NotFound();

                await _userRepository.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (implement your logging logic here)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        //Generating Token
        private string GetToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
            {
                throw new Exception("JWT configuration is missing");
            }

            var signingKey = Encoding.UTF8.GetBytes(key);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha512Signature
            );
            var subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
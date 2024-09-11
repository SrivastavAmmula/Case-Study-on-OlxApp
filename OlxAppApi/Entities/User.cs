using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OlxAppApi.Entities
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public String UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
        [JsonIgnore]
        public ICollection<Address>? Addresses { get; set; }
    }
}

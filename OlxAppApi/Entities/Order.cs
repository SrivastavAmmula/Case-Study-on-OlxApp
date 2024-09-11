using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OlxAppApi.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid(); // Automatically generate Guid
        public DateTime OrderDate { get; set; }


        [ForeignKey("Product")]
        public string ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        [ForeignKey("User")]
        
        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}

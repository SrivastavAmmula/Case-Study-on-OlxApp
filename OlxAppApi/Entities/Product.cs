using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OlxAppApi.Entities
{
    [Table("Items")]
    public class Product
    {
        [Key]
        public string ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        public string Description {  get; set; }
        public string Location { get; set; }
        public int Price { get; set; }

        //Foreign Keys
        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}

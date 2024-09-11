using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OlxAppApi.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public string CategoryId { get; set; } 
        public string CategooryName { get; set; }

        // Navigation properties
        //[JsonIgnore]
        //public ICollection<Product> Products { get; set; }

    }
}

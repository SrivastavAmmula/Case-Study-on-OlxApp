using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OlxAppApi.Entities
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid(); // Automatically generate Guid
        public  int Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; } // Relationship with User

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }

    }
}

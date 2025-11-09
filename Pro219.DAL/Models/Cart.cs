using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(100)]
        public string? SessionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public DateTime UpdateAt { get; set; }

        // Foreign key navigation property
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}


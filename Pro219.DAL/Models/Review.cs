using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int UniqueID { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public int? OrderItemId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Content { get; set; }

        [Required]
        public int Overall { get; set; } // Rating score (e.g., 1-5)

        public DateTime CreatedAt { get; set; }

        public bool? Delete { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public byte? Status { get; set; }

        [MaxLength(255)]
        public string? UpdateBy { get; set; }

        // Foreign key navigation properties
        [ForeignKey("OrderItemId")]
        public virtual OrderItem? OrderItem { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;
    }
}


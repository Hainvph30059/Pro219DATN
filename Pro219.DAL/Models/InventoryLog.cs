using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("InventoryLog")]
    public class InventoryLog
    {
        [Key]
        public int InventoryLogId { get; set; }

        [Required]
        public int VariantId { get; set; }

        [Required]
        public int ChangeQuantity { get; set; } // Positive for addition, negative for subtraction

        [MaxLength(500)]
        public string? Reason { get; set; }

        public DateTime CreateAt { get; set; }

        // Foreign key navigation property
        [ForeignKey("VariantId")]
        public virtual ProductVariant ProductVariant { get; set; } = null!;
    }
}


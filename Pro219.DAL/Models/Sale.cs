using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("Sale")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int? UpdateBy { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // e.g., "Percentage", "FixedAmount"

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SaleValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        // Foreign key navigation property
        [ForeignKey("UpdateBy")]
        public virtual User? User { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}


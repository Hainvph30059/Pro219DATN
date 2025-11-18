using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        public int? SaleId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        [MaxLength(255)]
        public string? UpdateBy { get; set; }

        // Foreign key navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; } = null!;

        [ForeignKey("SaleId")]
        public virtual Sale? Sale { get; set; }

        // Navigation properties
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}


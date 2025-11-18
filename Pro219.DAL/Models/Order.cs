using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ShippingAddressId { get; set; }

        public int? DiscountId { get; set; }

        public int? PaymentMethodId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderCode { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentStatus { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string OrderStatus { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime LastUpdate { get; set; }

        public bool? Delete { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public byte? Status { get; set; }

        [MaxLength(255)]
        public string? UpdateBy { get; set; }

        // Foreign key navigation properties
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } = null!;

        [ForeignKey("ShippingAddressId")]
        public virtual Address ShippingAddress { get; set; } = null!;

        [ForeignKey("DiscountId")]
        public virtual DiscountCode? DiscountCode { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        // Navigation properties
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}


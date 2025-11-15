using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class ProductVariantUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? ColorId { get; set; }

        public int? SizeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? ArrivalTime { get; set; }

        public bool IsActive { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



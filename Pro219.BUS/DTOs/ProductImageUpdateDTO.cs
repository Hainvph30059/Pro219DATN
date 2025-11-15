using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class ProductImageUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? ProductVariantId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsMain { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



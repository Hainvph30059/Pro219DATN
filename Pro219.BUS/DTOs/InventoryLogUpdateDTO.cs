using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class InventoryLogUpdateDTO
    {
        [Required]
        public int InventoryLogId { get; set; }

        [Required]
        public int VariantId { get; set; }

        [Required]
        public int ChangeQuantity { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



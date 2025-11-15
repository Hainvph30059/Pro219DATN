using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class PaymentMethodUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



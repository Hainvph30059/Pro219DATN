using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class SaleUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public decimal SaleValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



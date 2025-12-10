using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class DiscountCodeUpdateDTO
    {
        [Required]
        public int DiscountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string DiscountType { get; set; } = string.Empty;

        [Required]
        public decimal Value { get; set; }

        public decimal? MinOrderValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public byte Type { get; set; } = 1;

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



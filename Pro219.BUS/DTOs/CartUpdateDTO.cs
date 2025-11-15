using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class CartUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [MaxLength(100)]
        public string? SessionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public bool? Delete { get; set; }
    }
}



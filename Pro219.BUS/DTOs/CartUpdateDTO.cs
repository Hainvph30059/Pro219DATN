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

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



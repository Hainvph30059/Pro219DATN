using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class WishlistUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductVariantId { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class AddToCartDTO
    {
        [Required]
        public int VariantId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

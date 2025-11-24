using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class CartItemUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int VariantId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool? IsSelectedForCheckout { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



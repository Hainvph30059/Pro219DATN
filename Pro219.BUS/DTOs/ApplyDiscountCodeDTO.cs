namespace Pro219.API.DTOs
{
    public class ApplyDiscountCodeDTO
    {
        public decimal ShippingDiscount { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public byte? Type { get; set; } 
    }
}

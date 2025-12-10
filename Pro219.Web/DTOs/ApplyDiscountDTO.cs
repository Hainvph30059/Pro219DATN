namespace Pro219.Web.DTOs
{
    public class ApplyDiscountDTO
    {
        public decimal ShippingDiscount { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public byte? Type { get; set; }
    }
}

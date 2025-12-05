namespace Pro219.API.DTOs
{
    public class CheckoutItemDTO
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string ProductName { get; set; }
        
    }
}

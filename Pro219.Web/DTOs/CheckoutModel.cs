namespace Pro219.Web.DTOs
{
    public class CheckoutModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductVariantId { get; set; }
    }
}

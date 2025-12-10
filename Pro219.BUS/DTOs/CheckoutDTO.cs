namespace Pro219.API.DTOs
{
    public class CheckoutDTO
    {
        public int? PaymentType { get; set; }
        public string? URLPayment { get; set; }
        public string? OrderCode { get; set; }
        public int? OrderId { get; set; }
    }
}

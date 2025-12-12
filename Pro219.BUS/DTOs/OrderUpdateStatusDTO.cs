namespace Pro219.API.DTOs
{
    public class OrderUpdateStatusDTO
    {
        public int OrderId { get; set; }
        public byte Status { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public string OrderStatus { get; set; } = string.Empty;
    }
}


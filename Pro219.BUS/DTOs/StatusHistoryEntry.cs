namespace Pro219.API.DTOs
{
    public class StatusHistoryEntry
    {
        public int Index { get; set; }
        public byte Status { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string DateTime { get; set; } = string.Empty;
    }
}


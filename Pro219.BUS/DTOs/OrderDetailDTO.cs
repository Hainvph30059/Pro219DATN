using System.Collections.Generic;

namespace Pro219.API.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public decimal FinalAmount { get; set; }
        public string? Note { get; set; }
        public OrderDetailAddressDTO? Address { get; set; }
        public List<OrderDetailItemDTO> Items { get; set; } = new List<OrderDetailItemDTO>();
        public List<StatusHistoryEntry>? StatusHistory { get; set; }
    }

    public class OrderDetailAddressDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
    }

    public class OrderDetailItemDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}


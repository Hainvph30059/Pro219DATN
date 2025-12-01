using Pro219.DAL.Models;

namespace Pro219.Web.DTOs
{
    public class CartDTO
    {
    }

    public class CartItemWithProductDTO : CartItem
    {
        public string productName { get; set; } = string.Empty;

        public string ColorName { get; set; } = string.Empty;

        public string SizeName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}

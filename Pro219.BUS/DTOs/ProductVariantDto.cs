namespace Pro219.API.DTOs
{
    public class ProductVariantDto
    {
        public int VariantId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public string SizeName { get; set; }
        public int StockQuantity { get; set; }
        public decimal? VariantPrice { get; set; }
    }
}

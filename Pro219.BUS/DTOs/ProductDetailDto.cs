namespace Pro219.API.DTOs
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; } 
        public List<string> Images { get; set; }
        public List<ProductVariantDto> Variants { get; set; } = new();
        public List<ColorDto> UniqueColors { get; set; } = new();
        public List<SizeDto> UniqueSizes { get; set; } = new();
    }
}

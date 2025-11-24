namespace Pro219.Web.DTOs
{
    public class ProductShowDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> Images { get; set; } = new();
        public List<ColorDto> Colors { get; set; } = new();  
    }


}

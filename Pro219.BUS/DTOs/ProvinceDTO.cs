namespace Pro219.API.DTOs
{
    public class ProvinceDTO
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string Code { get; set; }
        public List<string> NameExtension { get; set; }
    }
}

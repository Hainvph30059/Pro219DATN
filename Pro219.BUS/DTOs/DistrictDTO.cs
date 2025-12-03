namespace Pro219.API.DTOs
{
    public class DistrictDTO
    {
        public int DistrictID { get; set; }
        public int ProvinceID { get; set; }
        public string DistrictName { get; set; }
        public string Code { get; set; }
        public List<string> NameExtension { get; set; }
    }
}

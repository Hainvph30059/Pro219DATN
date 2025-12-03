namespace Pro219.Web.DTOs
{
    public class WardDTO
    {
        public string WardCode { get; set; }
        public int DistrictID { get; set; }
        public string WardName { get; set; }
        public List<string> NameExtension { get; set; }
    }

    public class DistrictDTO
    {
        public int DistrictID { get; set; }
        public int ProvinceID { get; set; }
        public string DistrictName { get; set; }
        public string Code { get; set; }
        public List<string> NameExtension { get; set; }
    }

    public class ProvinceDTO
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string Code { get; set; }
        public List<string> NameExtension { get; set; }
    }

    public class ShippingFeeDTO
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public ShippingFeeData Data { get; set; }
    }

    public class ShippingFeeData
    {
        public decimal Total { get; set; }
    }
}

using Pro219.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int BrandId { get; set; }

        public int? SaleId { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(200, ErrorMessage = Constant.MessageValid.Max200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2000, ErrorMessage = Constant.MessageValid.Max2000)]
        public string? Description { get; set; }

        [Range(1000, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public decimal BasePrice { get; set; }

        public byte? Status { get; set; }
    }
}

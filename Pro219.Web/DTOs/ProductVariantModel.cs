using Pro219.Web.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.Web.DTOs
{
    public class ProductVariantModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int ColorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int SizeId { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(100, ErrorMessage = Constant.MessageValid.Max100)]
        public string SKU { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int StockQuantity { get; set; }


        [Range(1000, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public decimal Price { get; set; }

        public int? ArrivalTime { get; set; }

        public bool IsActive { get; set; }

        public byte? Status { get; set; }
    }
}

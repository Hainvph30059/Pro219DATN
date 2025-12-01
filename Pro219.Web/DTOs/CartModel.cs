using Pro219.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class CartModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constant.MessageValid.Required)]
        public int CustomerId { get; set; }

        public byte Status { get; set; }

        [MaxLength(100, ErrorMessage = Constant.MessageValid.Max100)]
        public string SesionId { get; set; }
    }

    public class AddCartModel
    {
        public int VariantId { get; set; }

        public int Quantity { get; set; }
    }
}

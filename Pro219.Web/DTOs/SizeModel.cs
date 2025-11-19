using Pro219.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class SizeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(50, ErrorMessage = Constant.MessageValid.Max50)]
        public string Name { get; set; } = string.Empty;

        public byte? Status { get; set; }
    }
}

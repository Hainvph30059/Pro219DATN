using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class ColorModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [RegularExpression(Constant.Regex.HexColor, ErrorMessage = Constant.MessageValid.HexColor)]
        public string HexCode { get; set; }

        public byte Status { get; set; }
    }
}

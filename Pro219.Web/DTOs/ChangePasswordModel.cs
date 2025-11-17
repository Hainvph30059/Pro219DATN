using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string NewHashPassword { get; set; }
    }
}

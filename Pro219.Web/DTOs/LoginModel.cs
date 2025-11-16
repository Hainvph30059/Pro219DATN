using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class LoginModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(255, ErrorMessage = Constant.MessageValid.Max255)]
        [EmailAddress(ErrorMessage = Constant.MessageValid.Email)]
        public string Username { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string PasswordHash { get; set; }
    }

    public class LoginAdminModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(255, ErrorMessage = Constant.MessageValid.Max255)]
        public string Username { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string PasswordHash { get; set; }

    }
}

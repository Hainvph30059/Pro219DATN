using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.Schema
{
    public class AuthSchema
    {
        public class LoginSchema
        {
            [Required(ErrorMessage = Constant.MessageValid.Required)]
            [MaxLength(255, ErrorMessage = Constant.MessageValid.Max255)]
            [EmailAddress(ErrorMessage = Constant.MessageValid.Email)]
            public string Username { get; set; }

            [Required(ErrorMessage = Constant.MessageValid.Required)]
            [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
            [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
            [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
            public string Password { get; set; }
        }
    }
}

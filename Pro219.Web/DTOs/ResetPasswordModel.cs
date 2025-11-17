using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(255, ErrorMessage = Constant.MessageValid.Max255)]
        [EmailAddress(ErrorMessage = Constant.MessageValid.Email)]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class CustomerModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(200, ErrorMessage = Constant.MessageValid.Max200)]
        [MinLength(2, ErrorMessage = Constant.MessageValid.Min2)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(11, ErrorMessage = Constant.MessageValid.PhoneNumberLength)]
        [MinLength(10, ErrorMessage = Constant.MessageValid.PhoneNumberLength)]
        [RegularExpression(Constant.Regex.PhoneNumber, ErrorMessage = Constant.MessageValid.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(100, ErrorMessage = Constant.MessageValid.Max100)]
        [EmailAddress(ErrorMessage = Constant.MessageValid.Email)]
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        public byte StatusByte { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;

namespace Pro219.Web.DTOs
{
    public class RegisterModel
    {
        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(200, ErrorMessage = Constant.MessageValid.Max200)]
        [MinLength(2, ErrorMessage = Constant.MessageValid.Min2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(11, ErrorMessage = Constant.MessageValid.PhoneNumberLength)]
        [MinLength(10, ErrorMessage = Constant.MessageValid.PhoneNumberLength)]
        [RegularExpression(Constant.Regex.PhoneNumber, ErrorMessage = Constant.MessageValid.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(100, ErrorMessage = Constant.MessageValid.Max100)]
        [EmailAddress(ErrorMessage = Constant.MessageValid.Email)]
        public string Email { get; set; }

        [DateNotInFutureAttribute]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(16, ErrorMessage = Constant.MessageValid.Password)]
        [MinLength(8, ErrorMessage = Constant.MessageValid.Password)]
        [RegularExpression(Constant.Regex.Password, ErrorMessage = Constant.MessageValid.Password)]
        public string PasswordHash { get; set; }
    }

    public class DateNotInFutureAttribute : ValidationAttribute
    {
        public DateNotInFutureAttribute()
        {
            ErrorMessage = Constant.MessageValid.DateFuture;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {   
                if (date > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                }
            }

            return ValidationResult.Success;
        }
    }
}


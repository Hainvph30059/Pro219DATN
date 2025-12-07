using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Pro219.Web.Constants;
using static Pro219.Web.DTOs.DateGreaterThanOrEqualAttribute;

namespace Pro219.Web.DTOs
{
    public class DiscountCodeModel
    {
        public int DiscountId { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(20, ErrorMessage = Constant.MessageValid.Max20)]
        public string DiscountType { get; set; } = string.Empty;

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [Range(1.0, (double)decimal.MaxValue, ErrorMessage = "Giá trị tối thiểu là 1.")]
        public decimal Value { get; set; } = 0;

        [Range(1.0, (double)decimal.MaxValue, ErrorMessage = "Giá trị tối thiểu là 1.")]
        public decimal? MinOrderValue { get; set; }

        [Range(1.0, (double)decimal.MaxValue, ErrorMessage = "Giá trị tối thiểu là 1.")]
        public decimal? MaxDiscountAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lần sử dụng tối thiểu là 1.")]
        public int? MaxUsage { get; set; } = 1;

        public int? UsageCount { get; set; } = 0;

        public bool? IsReusable { get; set; } = false;

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [FutureDate(ErrorMessage = "Ngày bắt đầu phải là một ngày trong tương lai.")]
        [DateGreaterThanOrEqual("EndDate", ErrorMessage = "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [DateMustBeGreaterThan("StartDate", ErrorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu.")]
        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class DateGreaterThanOrEqualAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanOrEqualAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var currentValueNullable = value as DateTime?;

            if (!currentValueNullable.HasValue)
                return ValidationResult.Success!;

            var currentValue = currentValueNullable.Value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Thuộc tính so sánh không tồn tại.");

            var comparisonValueNullable = property.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (!comparisonValueNullable.HasValue)
                return ValidationResult.Success!;

            var comparisonValue = comparisonValueNullable.Value;

            if (currentValue >= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? "Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            }

            return ValidationResult.Success!;
        }

    }

    public class DateMustBeGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateMustBeGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var currentValueNullable = value as DateTime?;
            if (!currentValueNullable.HasValue)
                return ValidationResult.Success!;
            var currentValue = currentValueNullable.Value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                throw new ArgumentException("Thuộc tính so sánh không tồn tại.");

            var comparisonValueNullable = property.GetValue(validationContext.ObjectInstance) as DateTime?;
            if (!comparisonValueNullable.HasValue)
                return ValidationResult.Success!;

            var comparisonValue = comparisonValueNullable.Value;

            if (currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }

            return ValidationResult.Success!;
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var dateNullable = value as DateTime?;

            if (!dateNullable.HasValue)
                return ValidationResult.Success!;

            var date = dateNullable.Value;

            if (date <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "Ngày bắt đầu phải là một ngày trong tương lai.");
            }

            return ValidationResult.Success!;
        }
    }
}

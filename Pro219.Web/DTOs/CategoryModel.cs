using Pro219.Web.Constants;
using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string VirtualId { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required(ErrorMessage = Constant.MessageValid.Required)]
        [MaxLength(200, ErrorMessage = Constant.MessageValid.Max200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = Constant.MessageValid.Max1000)]
        public string? Description { get; set; }

        public byte? Status { get; set; }
    }
}

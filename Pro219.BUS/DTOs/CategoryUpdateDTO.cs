using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        public int? ParentCategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



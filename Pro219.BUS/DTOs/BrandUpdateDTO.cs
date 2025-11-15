using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class BrandUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        public bool? Delete { get; set; }
    }
}



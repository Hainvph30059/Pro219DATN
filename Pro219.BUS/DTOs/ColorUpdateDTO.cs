using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class ColorUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? HexCode { get; set; }

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



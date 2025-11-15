using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class SizeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public byte? Status { get; set; }

        public bool? Delete { get; set; }
    }
}



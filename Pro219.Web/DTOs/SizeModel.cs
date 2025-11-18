using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class SizeModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public byte? Status { get; set; }
    }
}

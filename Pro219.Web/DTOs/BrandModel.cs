using System.ComponentModel.DataAnnotations;

namespace Pro219.Web.DTOs
{
    public class BrandModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; }

        public byte? Status { get; set; }
    }
}

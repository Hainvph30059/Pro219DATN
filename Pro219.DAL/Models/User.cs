using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro219.DAL.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; }

        public byte? Status { get; set; }

    }
}


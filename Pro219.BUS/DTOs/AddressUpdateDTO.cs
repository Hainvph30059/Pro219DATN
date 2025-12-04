using System.ComponentModel.DataAnnotations;

namespace Pro219.API.DTOs
{
    public class AddressUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string District { get; set; } = string.Empty; 

        public string CityName { get; set; } = string.Empty;

        public string DistrictName { get; set; } = string.Empty;

        public string StreetName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? OtherInfo { get; set; }

        public bool IsDefault { get; set; }

        public bool? Delete { get; set; }
    }
}



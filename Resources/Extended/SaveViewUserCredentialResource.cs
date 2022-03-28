using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources.Extended
{
    public class SaveUserCredentialResource
    {
        public long UserId { get; set; }

        [Required]
        [MaxLength(128)]
        public string FullName { get; set; } = null!;

        [MaxLength(128)]
        public string Password { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        public int OrderNo { get; set; }
        
        [Required]
        public long TeamId { get; set; }

        [Required]
        [MaxLength(45)]
        public string Role { get; set; } = null!;

        [MaxLength(50)]
        public string? PhoneNumber { get; set; } = null!;

        [MaxLength(128)]
        [EmailAddress]
        public string? Email { get; set; } = null!;

        [MaxLength(128)]
        public string? Address { get; set; } = null!;

        [MaxLength(500)]
        public string? Avatar { get; set; } = null!;

        [MaxLength(128)]
        public string? AboutMe { get; set; } = null!;

        [MaxLength(128)]
        public string? IdNo { get; set; } = null!;

        [MaxLength(128)]
        public string? IssuedTo { get; set; } = null!;

        [Required]
        public long CreatedBy { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(128)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = null!;

        [MaxLength(128)]
        public string? StaffId { get; set; }

        [Required]
        [MaxLength(128)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        [MaxLength(128)]
        public string? IdNo { get; set; }

        [MaxLength(128)]
        public string? IssuedOn { get; set; }

        [MaxLength(128)]
        public string? Address { get; set; }

        [MaxLength(128)]
        public string? AboutMe { get; set; }

        [MaxLength(500)]
        public string? Avatar { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

    }
}

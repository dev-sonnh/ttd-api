using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class UserResource
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? StaffId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? IdNo { get; set; }
        public string? IssuedOn { get; set; }
        public string? Address { get; set; }
        public string? AboutMe { get; set; }
        public string? Avatar { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}

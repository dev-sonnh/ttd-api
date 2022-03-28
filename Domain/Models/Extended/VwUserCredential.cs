namespace TTDesign.API.Domain.Models.Extended
{
    public class VwUserCredential
    {
        public long UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        public long TeamId { get; set; }

        public int? OrderNo { get; set; } = 0;

        public string Role { get; set; } = null!;

        public string? StaffId { get; set; } = null!;

        public string? PhoneNumber { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Address { get; set; } = null!;

        public string? Avatar { get; set; } = null!;

        public string? AboutMe { get; set; } = null!;

        public string? IdNo { get; set; } = null!;

        public string? IssuedTo { get; set; } = null!;

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}

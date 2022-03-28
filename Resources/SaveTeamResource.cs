using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources
{
    public class SaveTeamResource
    {
        [Required]
        [MaxLength(128)]
        public string TeamName { get; set; } = null!;

        [Required]
        [MaxLength(45)]
        public string TeamCode { get; set; } = null!;

        [MaxLength(500)]
        public string? TeamDescription { get; set; }

        [Required]
        public ulong IsDepartment { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}

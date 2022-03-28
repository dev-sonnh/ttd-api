using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources
{
    public class SaveTimesheetResource
    {
        [Required]
        public long? UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources.Extended
{
    public class UpdateTimesheetByFingerprintMachineResource
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }
    }
}

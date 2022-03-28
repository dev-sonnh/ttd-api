using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class TimesheetResource
    {
        public long TimesheetId { get;set; }
        public long? UserId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? Note { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Overtime> Overtimes { get; set; }
        public virtual ICollection<TimesheetTask> TimesheetTasks { get; set; }
    }
}

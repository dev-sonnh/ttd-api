namespace TTDesign.API.Resources
{
    public class TeamResource
    {
        public long TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string TeamCode { get; set; } = null!;
        public string? TeamDescription { get; set; }
        public ulong IsDepartment { get; set; }
        //public long CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public long? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }
}

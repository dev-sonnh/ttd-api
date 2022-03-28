namespace TTDesign.API.Resources
{
    public class UserSettingResource
    {
        public long UserSettingId { get; set; }
        public long UserId { get; set; }
        public ulong EmailNotification { get; set; }
        public string Timezone { get; set; } = null!;
        public string Language { get; set; } = null!;
    }
}

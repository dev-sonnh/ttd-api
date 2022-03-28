using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources
{
    public class SaveUserSettingResource
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public ulong EmailNotification { get; set; }

        [MaxLength(128)]
        public string Timezone { get; set; } = null!;

        [MaxLength(50)]
        public string Language { get; set; } = null!;

    }
}

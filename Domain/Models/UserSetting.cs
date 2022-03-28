using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{ 
    public partial class UserSetting
    {
        public long UserSettingId { get; set; }
        public long UserId { get; set; }
        public ulong EmailNotification { get; set; }
        public string Timezone { get; set; } = null!;
        public string Language { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class UserRole
    {
        public long UserRoleId { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

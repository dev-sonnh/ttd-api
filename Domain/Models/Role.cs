using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

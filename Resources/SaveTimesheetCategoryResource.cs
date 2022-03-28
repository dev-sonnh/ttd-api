﻿using TTDesign.API.Domain.Models;

namespace TTDesign.API.Resources
{
    public class SaveTimesheetCategoryResource
    {

        public long TeamId { get; set; }
        public string Name { get; set; } = null!;
        public ulong IsActive { get; set; }
        public ulong IsPublic { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

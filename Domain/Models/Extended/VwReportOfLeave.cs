using System;
using System.Collections.Generic;

namespace TTDesign.API.Domain.Models.Extended
{
    public partial class VwReportOfLeave
    {
        public long UserId { get; set; }
        public double? ThisYearTotal { get; set; }
        public double? ThisAnnualLeft { get; set; }
        public double? ThisSummerLeft { get; set; }
        public double LastYearLeft { get; set; }
        public double TotalUse { get; set; }
        public double AnnualUse { get; set; }
        public double SummerUse { get; set; }
        public double RestOfLeaveDays { get; set; }
    }
}

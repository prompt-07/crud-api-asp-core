using System;
using System.Collections.Generic;

namespace TenDotAPI.Models
{
    public partial class ApprovalMaster
    {
        public int? LowerAmtLimit { get; set; }
        public int? UpperAmtLimit { get; set; }
        public string TenDotHirarchy { get; set; }
        public string Approver { get; set; }
    }
}

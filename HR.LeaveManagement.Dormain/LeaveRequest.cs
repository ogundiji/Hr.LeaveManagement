using HR.LeaveManagement.Dormain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Dormain
{
    public class LeaveRequest : BaseDormainEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComment { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
    }
}

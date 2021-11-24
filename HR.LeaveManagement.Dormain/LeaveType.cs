using HR.LeaveManagement.Dormain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Dormain
{
    public class LeaveType : BaseDormainEntity
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}

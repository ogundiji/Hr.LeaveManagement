using HR.LeaveManagement.Dormain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Dormain
{
    public class LeaveAllocation:BaseDormainEntity
    {
      
        public int NumberOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
using Hr.LeaveManagement.Dormain.Common;

namespace HR.LeaveManagement.Dormain
{
    public class LeaveAllocation:BaseDormainEntity
    {
      
        public int NumberOfDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
        public string EmployeeId { get; set; }
    }
}
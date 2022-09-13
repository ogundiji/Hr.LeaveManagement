using Hr.LeaveManagement.Dormain.Common;

namespace HR.LeaveManagement.Dormain
{
    public class LeaveType : BaseDormainEntity
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}

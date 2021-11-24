using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Dormain.Common
{
    public abstract class BaseDormainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
    }
}

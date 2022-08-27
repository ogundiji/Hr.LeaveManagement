using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hr.LeaveManagement.MVC.Models
{
    public class LeaveAllocationVm
    {
        public int Id { get; set; }
        [Display(Name = "Number Of Days")]

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }

        public LeaveTypeVm LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
    }

    public class CreateLeaveAllocationVM
    {
        public int LeaveTypeId { get; set; }
    }

    public class UpdateLeaveAllocationVM
    {
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        [Range(1, 50, ErrorMessage = "Enter Valid Number")]
        public int NumberOfDays { get; set; }
        public LeaveTypeVm LeaveType { get; set; }

    }

    public class ViewLeaveAllocationsVM
    {
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVm> LeaveAllocations { get; set; }
    }
}

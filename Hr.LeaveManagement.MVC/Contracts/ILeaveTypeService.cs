using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVm>> GetLeaveTypes();
        Task<LeaveTypeVm> GetLeaveTypeDetails(int Id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveType(LeaveTypeVm leaveType);
        Task<Response<int>> DeleteLeaveType(int Id);
    }
}
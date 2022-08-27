using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;
using HR.LeaveManagement.Dormain;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
       // Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
       // Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
        Task<LeaveRequest> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);

    }
}

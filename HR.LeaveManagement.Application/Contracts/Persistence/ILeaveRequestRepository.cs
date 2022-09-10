using HR.LeaveManagement.Dormain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository:IRepository<LeaveRequest>
    {
        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approval);
        Task<LeaveRequest> GetLeaveRequestWithDetails(int Id);
        Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string Id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    }
}

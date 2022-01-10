using HR.LeaveManagement.Dormain;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveRequestRepository:IRepository<LeaveRequest>
    {
        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approval);
        Task<LeaveRequest> GetLeaveRequestWithDetails(int Id);
        Task<LeaveRequest> GetLeaveRequestsWithDetails();
    }
}

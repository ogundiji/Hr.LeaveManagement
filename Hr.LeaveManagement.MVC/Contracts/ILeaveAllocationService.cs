using Hr.LeaveManagement.MVC.Services.Base;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}

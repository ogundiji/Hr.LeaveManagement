using HR.LeaveManagement.Dormain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository:IRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<bool> AllocationExists(string userId, int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId);
    }
}

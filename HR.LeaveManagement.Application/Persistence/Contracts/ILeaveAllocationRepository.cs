using HR.LeaveManagement.Dormain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Persistence.Contracts
{
    public interface ILeaveAllocationRepository:IRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id);

        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

    }
}

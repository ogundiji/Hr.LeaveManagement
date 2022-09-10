using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly LeaveManagementDbContext _dbContext;
        public LeaveAllocationRepository(LeaveManagementDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.LeaveAllocations.AddRangeAsync(allocations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync(x=>x.EmployeeId==userId
            && x.LeaveTypeId==leaveTypeId 
            && x.Period==period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations.Where(q => q.EmployeeId == userId)
              .Include(q => q.LeaveType)
              .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id)
        {
            var leaveAllocation = await _dbContext.LeaveAllocations
                           .Include(x => x.LeaveType)
                           .FirstOrDefaultAsync(x => x.Id == Id);

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
                                       && q.LeaveTypeId == leaveTypeId);
        }
    }
}

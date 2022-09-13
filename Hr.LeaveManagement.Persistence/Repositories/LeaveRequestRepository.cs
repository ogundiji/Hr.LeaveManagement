using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _dbContext;
        public LeaveRequestRepository(LeaveManagementDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approval)
        {
            leaveRequest.Approved = approval;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
           
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .ToListAsync();
            return leaveRequests;

        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int Id)
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return leaveRequests;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string Id)
        {
            var leaveRequests = await _dbContext.LeaveRequests.Where(q => q.RequestingEmployeeId == Id)
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }
    }
}

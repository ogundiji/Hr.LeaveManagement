using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;

namespace Hr.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
    {
        private readonly LeaveManagementDbContext _dbContext;
        public LeaveTypeRepository(LeaveManagementDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

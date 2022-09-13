using Hr.LeaveManagement.Dormain.Common;
using HR.LeaveManagement.Dormain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence
{
    public class LeaveManagementDbContext:AuditableDbContext
    {
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}

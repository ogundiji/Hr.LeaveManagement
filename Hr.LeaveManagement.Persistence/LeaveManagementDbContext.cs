﻿using HR.LeaveManagement.Dormain;
using HR.LeaveManagement.Dormain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Persistence
{
    public class LeaveManagementDbContext:DbContext
    {
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<BaseDormainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                if(entry.State== EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}

using Hr.LeaveManagement.Identity.Configurations;
using Hr.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hr.LeaveManagement.Identity
{
    public class LeaveManagementIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public LeaveManagementIdentityDbContext(DbContextOptions<LeaveManagementIdentityDbContext>options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());

        }
    }
}

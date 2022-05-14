using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hr.LeaveManagement.Identity.Configurations
{
    public class RoleConfiguration:IEntityTypeConfiguration<IdentityRole>
    {
        
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "c8c6e6a9-4976-4986-9fa0-c00329749085",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole()
                {
                    Id = "7998854b-13d7-4ecd-9adb-be5a57ddeb2e",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });

        }
    }
}

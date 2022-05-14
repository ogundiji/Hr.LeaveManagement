using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hr.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "7998854b-13d7-4ecd-9adb-be5a57ddeb2e",
                    UserId = "db13c806-dd7f-41e1-b7ae-196e1ffa465a"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "c8c6e6a9-4976-4986-9fa0-c00329749085",
                    UserId = "f75e73b6-ad4f-4fad-8dde-a1161a0d9a6c"
                });
        }
    }
}

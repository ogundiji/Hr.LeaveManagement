using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hr.LeaveManagement.Identity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}

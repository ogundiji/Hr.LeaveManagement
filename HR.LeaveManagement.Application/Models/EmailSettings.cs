using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Models
{
    public class EmailSettings
    {
        public string ApiKey { get; set; }
        public string FromAddresses { get; set; }
        public string FromName { get; set; }
    }
}

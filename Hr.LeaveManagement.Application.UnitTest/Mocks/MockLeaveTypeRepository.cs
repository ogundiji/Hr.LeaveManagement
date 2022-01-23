using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Application.UnitTest.Mocks
{
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id=1,
                    DefaultDays=10,
                    Name="Test Vacation"
                },
                new LeaveType
                {
                    Id=2,
                    DefaultDays=15,
                    Name="Test Sick"
                }
            };

            //setup a new repo
            var mockRepo = new Mock<ILeaveTypeRepository>();

            //setup for GetAll inside the repository and ensure that the returns type confirm to the test data
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
              {
                  leaveTypes.Add(leaveType);
                  return leaveType;
              });

            return mockRepo;
            
        }
    }
}

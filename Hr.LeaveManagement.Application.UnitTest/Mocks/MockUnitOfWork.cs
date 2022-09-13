using HR.LeaveManagement.Application.Contracts.Persistence;
using Moq;

namespace Hr.LeaveManagement.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveTypeRepo.Object);

            return mockUow;
        }
    }
}

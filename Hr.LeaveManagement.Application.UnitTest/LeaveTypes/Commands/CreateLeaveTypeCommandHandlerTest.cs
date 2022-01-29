using AutoMapper;
using FluentValidation;
using Hr.LeaveManagement.Application.UnitTest.Mocks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Hr.LeaveManagement.Application.UnitTest.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly CreateLeaveTypeDto _leaveTypedto;
        private readonly CreateLeaveTypeCommandHandler _handler;
        public CreateLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

            _leaveTypedto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Test Dto"
            };

        }

        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand { leaveTypeDto = _leaveTypedto }, CancellationToken.None);
            result.ShouldBeOfType<int>();
            var leaveTypes = await  _mockRepo.Object.GetAll();

            leaveTypes.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Inavlid_LeaveType_addded()
        {
            _leaveTypedto.DefaultDays = -1;
            ValidationException ex = await Should.ThrowAsync<ValidationException>
                (
                  async () =>
                  await _handler.Handle(new CreateLeaveTypeCommand { leaveTypeDto = _leaveTypedto }, CancellationToken.None)
                );

            var leaveTypes = await _mockRepo.Object.GetAll();


            leaveTypes.Count.ShouldBe(2);

        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Dormain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {

        private readonly ILeaveAllocationRepository leaveAllocation;
        private readonly IMapper _mapper;
        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocation, IMapper mapper)
        {
            this.leaveAllocation = leaveAllocation;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leave = _mapper.Map<LeaveAllocation>(request.leaveAllocationDto);
            var leaveResponse = await leaveAllocation.Add(leave);

            return leaveResponse.Id;
        }

    }
}

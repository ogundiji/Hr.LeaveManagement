using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Dormain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly ILeaveTypeRepository _leaveType;
        private readonly ILeaveRequestRepository leaveRequest;
        private readonly IMapper _mapper;
        public CreateLeaveAllocationCommandHandler(ILeaveRequestRepository leaveRequest, ILeaveTypeRepository leaveType, IMapper mapper)
        {
            this.leaveRequest = leaveRequest;
            _leaveType = leaveType;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validators = new CreateLeaveAllocationDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

           
            var leave = _mapper.Map<LeaveRequest>(request.leaveAllocationDto);
            var leaveResponse = await leaveRequest.Add(leave);

            return leaveResponse.Id;
        }

    }
}

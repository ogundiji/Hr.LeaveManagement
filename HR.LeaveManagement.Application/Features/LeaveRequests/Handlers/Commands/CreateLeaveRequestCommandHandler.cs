using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Dormain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequest, IMapper mapper, ILeaveTypeRepository leaveType)
        {
            _leaveRequest = leaveRequest;
            _mapper = mapper;
            _leaveType = leaveType;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validators = new CreateLeaveRequestDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveRequestDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);
             
            var leave = _mapper.Map<LeaveRequest>(request.leaveRequestDto);
            var leaveResponse = await _leaveRequest.Add(leave);

            return leaveResponse.Id;
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand,BaseCommandResponse>
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

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validators = new CreateLeaveAllocationDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

           
            var leave = _mapper.Map<LeaveRequest>(request.leaveAllocationDto);
            var leaveResponse = await leaveRequest.Add(leave);

            response.Success = true;
            response.Message = "Creation Sucessfull";
            response.Id = leave.Id;

            return response;
        }

    }
}

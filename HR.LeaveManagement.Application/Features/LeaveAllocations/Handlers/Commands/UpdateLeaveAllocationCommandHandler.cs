using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand,Unit>
    {
        private readonly ILeaveTypeRepository _leaveType;
        private readonly ILeaveAllocationRepository leaveAllocation;
        private readonly IMapper _mapper;
        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocation, ILeaveTypeRepository leaveType, IMapper mapper)
        {
            this.leaveAllocation = leaveAllocation;
            _leaveType = leaveType;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validators = new UpdateLeaveAllocationDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.UpdateLeaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leave = await leaveAllocation.Get(request.UpdateLeaveAllocationDto.Id);
            _mapper.Map(request.UpdateLeaveAllocationDto, leave);

            await leaveAllocation.Update(leave);

            return Unit.Value;
        }
    }
}

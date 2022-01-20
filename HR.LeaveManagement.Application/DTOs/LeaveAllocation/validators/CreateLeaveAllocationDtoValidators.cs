using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators
{
    public class CreateLeaveAllocationDtoValidators:AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveAllocationDtoValidators(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDtoValidators(_leaveTypeRepository));
        }
    }
}

using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;
using System;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators
{
    public class CreateLeaveAllocationDtoValidators:AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public CreateLeaveAllocationDtoValidators(ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new ILeaveAllocationDtoValidators(_leaveTypeRepository));
        }
    }
}

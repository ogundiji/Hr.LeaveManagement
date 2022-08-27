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

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExist = await _leaveTypeRepository.isExist(id);
                    return !leaveTypeExist;
                }).WithMessage("{PropertyName} does not exist");
        }
    }
}

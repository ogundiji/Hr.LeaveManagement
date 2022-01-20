using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators
{
    public class ILeaveAllocationDtoValidators:AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveAllocationDtoValidators(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.NumberOfDays)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero");

            RuleFor(p => p.Period)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(DateTime.Now.Year).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.isExist(id);
                    return !leaveTypeExists;
                }).WithMessage("{PropertyName} does not exist");
        }
    }
}

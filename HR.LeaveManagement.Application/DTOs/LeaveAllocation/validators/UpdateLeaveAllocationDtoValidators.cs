using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;


namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators
{
    public class UpdateLeaveAllocationDtoValidators:AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveAllocationDtoValidators(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveAllocationDtoValidators(_leaveTypeRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

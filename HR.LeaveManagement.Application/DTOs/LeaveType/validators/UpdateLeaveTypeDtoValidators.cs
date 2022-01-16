using FluentValidation;


namespace HR.LeaveManagement.Application.DTOs.LeaveType.validators
{
    public class UpdateLeaveTypeDtoValidators: AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidators()
        {
            Include(new ILeaveTypeValidator());

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

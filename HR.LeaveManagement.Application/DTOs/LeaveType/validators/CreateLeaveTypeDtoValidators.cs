using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.validators
{
    public class CreateLeaveTypeDtoValidators:AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidators()
        {
            Include(new ILeaveTypeValidator());
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.validators
{
    public class ILeaveTypeValidator:AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not be less than {ComparisionValue} characters");

            RuleFor(p => p.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is Required")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
                .LessThan(100).WithMessage("{PropertyName} must be less than {ComparisonValue}");
        }
    }
}

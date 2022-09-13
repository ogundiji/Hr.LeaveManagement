using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidators();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leaveType = await unitOfWork.LeaveTypeRepository.Get(request.leaveTypeDto.Id);
            _mapper.Map(request.leaveTypeDto, leaveType);

            await unitOfWork.LeaveTypeRepository.Update(leaveType);

            return Unit.Value; 
        }
    }
}

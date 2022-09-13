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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validators = new UpdateLeaveAllocationDtoValidators(unitOfWork.LeaveTypeRepository);
            var validationResult = await validators.ValidateAsync(request.UpdateLeaveAllocationDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var leave = await unitOfWork.LeaveAllocationRepository.Get(request.UpdateLeaveAllocationDto.Id);
            _mapper.Map(request.UpdateLeaveAllocationDto, leave);

            await unitOfWork.LeaveAllocationRepository.Update(leave);

            return Unit.Value;
        }
    }
}

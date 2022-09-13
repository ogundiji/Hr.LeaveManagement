using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
           
            var leaveRequest = await unitOfWork.LeaveRequestRepository.Get(request.Id);
            if (request.leaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidators(unitOfWork.LeaveTypeRepository);
                var validateResult = await validator.ValidateAsync(request.leaveRequestDto);
                if (validateResult.IsValid == false)
                    throw new ValidationException(validateResult);

                _mapper.Map(request.leaveRequestDto, leaveRequest);

                await unitOfWork.LeaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);

                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await unitOfWork.LeaveAllocationRepository.Update(allocation);
                }
            }
         
            return Unit.Value;
        }
    }
}

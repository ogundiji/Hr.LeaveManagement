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
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveType;
        
        private readonly IMapper _mapper;
        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveType, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            _leaveType = leaveType;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
           
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);
            if (request.leaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidators(_leaveType);
                var validateResult = await validator.ValidateAsync(request.leaveRequestDto);
                if (validateResult.IsValid == false)
                    throw new ValidationException(validateResult);

                _mapper.Map(request.leaveRequestDto, leaveRequest);

                await _leaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);

                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await leaveAllocationRepository.Update(allocation);
                }
            }
         
            return Unit.Value;
        }
    }
}

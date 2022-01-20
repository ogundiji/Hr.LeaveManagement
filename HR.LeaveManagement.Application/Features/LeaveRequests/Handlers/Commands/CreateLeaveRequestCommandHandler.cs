using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand,BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IMapper _mapper;
       

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequest, IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveType)
        {
            _leaveRequest = leaveRequest;
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveType = leaveType;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validators = new CreateLeaveRequestDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveRequestDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);
             
            var leave = _mapper.Map<LeaveRequest>(request.leaveRequestDto);
            var leaveResponse = await _leaveRequest.Add(leave);

            response.Success = true;
            response.Message = "Creation Sucessfull";
            response.Id = leave.Id;

            var email = new Email
            {
                To="employee@org.com",
                Body= $"your leave request for {request.leaveRequestDto.StartDate:D} to {request.leaveRequestDto.EndDate}"
                + $"has been successfully submitted",
                Subject="Leave Request Submited"
            };

            try
            {
                await _emailSender.SendEmail(email);
            }
            catch(Exception ex)
            {

            }

            return response;
        }
    }
}

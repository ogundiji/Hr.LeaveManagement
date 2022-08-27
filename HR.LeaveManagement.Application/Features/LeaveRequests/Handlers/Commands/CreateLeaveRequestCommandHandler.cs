using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.validators;
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
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand,BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly ILeaveTypeRepository _leaveType;
        private readonly IMapper _mapper;
        
       

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequest,ILeaveAllocationRepository leaveAllocationRepository,
            IHttpContextAccessor httpContextAccessor,IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveType)
        {
            _leaveRequest = leaveRequest;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _mapper = mapper;
            _leaveType = leaveType;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validators = new CreateLeaveRequestDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveRequestDto);
            var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "uid")?.Value;

            var allocation = await leaveAllocationRepository.GetUserAllocations(userId, request.leaveRequestDto.LeaveId);
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.leaveRequestDto.LeaveId),
                    "You do not have any allocations for this leave type."));
            }
            else
            {
                int daysRequested = (int)(request.leaveRequestDto.EndDate - request.leaveRequestDto.StartDate).TotalDays;
                if (daysRequested > allocation.NumberOfDays)
                {
                    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(request.leaveRequestDto.EndDate), "You do not have enough days for this request"));
                }
            }


            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();  
            }
            else
            {
                var leave = _mapper.Map<LeaveRequest>(request.leaveRequestDto);
                var leaveResponse = await _leaveRequest.Add(leave);
                leave.RequestingEmployeeId = userId;

                response.Success = true;
                response.Message = "Creation Sucessfull";
                response.Id = leave.Id;

               

                try
                {
                    var emailAddress = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

                    var email = new Email
                    {
                        To = emailAddress,
                        Body = $"your leave request for {request.leaveRequestDto.StartDate:D} to {request.leaveRequestDto.EndDate}"
                        + $"has been successfully submitted",
                        Subject = "Leave Request Submited"
                    };

                    await _emailSender.SendEmail(email);
                }
                catch (Exception ex)
                {
                   //log exception
                }
            }
               
             
           

            return response;
        }
    }
}

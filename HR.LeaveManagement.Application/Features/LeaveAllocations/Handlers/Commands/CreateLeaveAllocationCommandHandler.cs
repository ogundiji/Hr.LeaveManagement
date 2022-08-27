using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Dormain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Identity;
using System.Linq;
using System.Collections.Generic;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand,BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveType;
        private readonly ILeaveRequestRepository leaveRequest;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IUserService userService;
        public CreateLeaveAllocationCommandHandler(ILeaveRequestRepository leaveRequest, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveType, IMapper mapper,IUserService userService)
        {
            this.leaveRequest = leaveRequest;
            _leaveType = leaveType;
            _mapper = mapper;
            this.userService = userService;
            _leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validators = new CreateLeaveAllocationDtoValidators(_leaveType);
            var validationResult = await validators.ValidateAsync(request.leaveAllocationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await _leaveType.Get(request.leaveAllocationDto.LeaveTypeId);
                var employees = await userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<LeaveAllocation>();

                foreach(var emp in employees)
                {
                    if (await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        Period = period,
                        NumberOfDays = leaveType.DefaultDays
                    });
                }

                await _leaveAllocationRepository.AddAllocations(allocations);

                response.Success = true;
                response.Message = "Creation Sucessfull";

            }
                
            return response;
        }

    }
}

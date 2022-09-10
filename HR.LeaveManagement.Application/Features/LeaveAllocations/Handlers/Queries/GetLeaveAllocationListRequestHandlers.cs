using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Dormain;
using HR.LeaveManagement.Application.Constants;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandlers : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocation;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public GetLeaveAllocationListRequestHandlers(ILeaveAllocationRepository leaveAllocation,
              IHttpContextAccessor httpContextAccessor,
            IUserService userService,IMapper _mapper)
        {
            this._mapper = _mapper;
            this.leaveAllocation = leaveAllocation;
            this._httpContextAccessor = httpContextAccessor;
            this._userService = userService;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllocations = await leaveAllocation.GetLeaveAllocationsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var alloc in allocations)
                {
                    alloc.Employee = employee;
                }
            }
            else
            {
                leaveAllocations = await leaveAllocation.GetLeaveAllocationsWithDetails();
                allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var req in allocations)
                {
                    req.Employee = await _userService.GetEmployee(req.EmployeeId);
                }
            }

            return allocations;
        }
    }
}

using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Constants;
using HR.LeaveManagement.Dormain;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandlers : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;
        private readonly IMapper _mapper;
       
        public GetLeaveRequestListRequestHandlers(ILeaveRequestRepository leaveRequest,IHttpContextAccessor httpContextAccessor,IUserService userService, IMapper mapper)
        {
            _leaveRequest = leaveRequest;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
            _mapper = mapper;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirst(
                    q => q.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await _leaveRequest.GetLeaveRequestWithDetails(userId);

                var employee = await userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.employee = employee;
                }
            }
            else
            {
                leaveRequests = await _leaveRequest.GetLeaveRequestsWithDetails();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.employee = await userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}

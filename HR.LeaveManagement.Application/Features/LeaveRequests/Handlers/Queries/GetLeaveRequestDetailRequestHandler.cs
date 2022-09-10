using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Identity;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly IUserService userService;
        private readonly IMapper _mapper;
      
        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequest, IUserService userService, IMapper mapper)
        {
            _leaveRequest = leaveRequest;
            this.userService = userService;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await _leaveRequest.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.employee = await userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }

    }
}
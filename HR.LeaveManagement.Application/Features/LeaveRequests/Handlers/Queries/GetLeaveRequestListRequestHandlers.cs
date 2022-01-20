using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandlers : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
        private readonly IMapper _mapper;
        public GetLeaveRequestListRequestHandlers(ILeaveRequestRepository leaveRequest, IMapper mapper)
        {
            _leaveRequest = leaveRequest;
            _mapper = mapper;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leave = await _leaveRequest.GetLeaveRequestsWithDetails();
            return _mapper.Map<List<LeaveRequestListDto>>(leave);
        }
    }
}

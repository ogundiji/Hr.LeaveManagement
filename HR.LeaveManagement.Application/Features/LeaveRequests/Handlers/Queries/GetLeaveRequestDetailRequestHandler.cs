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
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IMapper _mapper;
      
        public GetLeaveRequestDetailRequestHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            _mapper = mapper;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = _mapper.Map<LeaveRequestDto>(await unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.employee = await userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }

    }
}
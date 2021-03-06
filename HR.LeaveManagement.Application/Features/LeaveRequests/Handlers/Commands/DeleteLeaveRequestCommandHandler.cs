using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository _leaveRequest;
       
        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequest)
        {
            _leaveRequest = leaveRequest;
        }
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leave =await _leaveRequest.Get(request.Id);
            if (leave == null)
                throw new NotFoundException(nameof(leave), request.Id);

            await _leaveRequest.Delete(leave);

            return Unit.Value;
        }
    }
}

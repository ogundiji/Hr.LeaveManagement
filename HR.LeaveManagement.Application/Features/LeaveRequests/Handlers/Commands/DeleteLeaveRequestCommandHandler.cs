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

        private readonly IUnitOfWork UnitOfWork;
        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

       

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leave =await UnitOfWork.LeaveRequestRepository.Get(request.Id);
            if (leave == null)
                throw new NotFoundException(nameof(leave), request.Id);

            await UnitOfWork.LeaveRequestRepository.Delete(leave);

            return Unit.Value;
        }
    }
}

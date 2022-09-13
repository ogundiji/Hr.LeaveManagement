using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
    {
        private readonly IUnitOfWork UnitOfWork;
        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leavetype = await UnitOfWork.LeaveTypeRepository.Get(request.Id);
            if (leavetype == null)
                throw new NotFoundException(nameof(leavetype), request.Id);

            await UnitOfWork.LeaveTypeRepository.Delete(leavetype);

            return Unit.Value;
        }
    }
}

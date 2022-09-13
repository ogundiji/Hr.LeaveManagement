using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly IUnitOfWork UnitOfWork;

        public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

       

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await UnitOfWork.LeaveAllocationRepository.Get(request.Id);
            if (leaveAllocation == null)
                throw new NotFoundException(nameof(leaveAllocation), request.Id);

            await UnitOfWork.LeaveAllocationRepository.Delete(leaveAllocation);

            return Unit.Value;
        }
    }
}

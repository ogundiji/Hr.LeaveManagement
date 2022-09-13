using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveTypeDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveDetailRecord = await unitOfWork.LeaveTypeRepository.Get(request.Id);

            if (leaveDetailRecord == null)
                throw new NotFoundException(nameof(leaveDetailRecord), request.Id);

            return _mapper.Map<LeaveTypeDto>(leaveDetailRecord);
        }
    }
}

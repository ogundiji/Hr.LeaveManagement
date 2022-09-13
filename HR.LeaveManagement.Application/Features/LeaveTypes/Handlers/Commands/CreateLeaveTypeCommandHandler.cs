using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType.validators;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Dormain;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidators();
            var validationResult = await validator.ValidateAsync(request.leaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {
                var leaveRequest = _mapper.Map<LeaveType>(request.leaveTypeDto);
                var leave = await unitOfWork.LeaveTypeRepository.Add(leaveRequest);

                response.Success = true;
                response.Message = "Creation Sucessfull";
                response.Id = leave.Id;  
            }

            return response;
        }
    }
}

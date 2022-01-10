using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;


namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class UpdateLeaveAllocationCommand:IRequest<Unit>
    {
       public UpdateLeaveAllocationDto UpdateLeaveAllocationDto { get; set; }
    } 
}

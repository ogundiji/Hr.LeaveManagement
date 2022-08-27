using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveRequestController : ControllerBase
    {
        public readonly IMediator _mediator;
        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int Id)
        {
            var leaveType = await _mediator.Send(new GetLeaveRequestDetailRequest() { Id = Id });
            return Ok(leaveType);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BaseCommandResponse>> post([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            var command = new CreateLeaveRequestCommand() { leaveRequestDto = createLeaveRequestDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> put(int Id,[FromBody] UpdateLeaveRequestDto leaveRequestDto)
        {
            var command = new UpdateLeaveRequestCommand() { leaveRequestDto=leaveRequestDto,Id=Id };
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("changeapproval/{Id}")]
        public async Task<ActionResult> ChangeApproval(int Id, [FromBody] ChangeLeaveRequestApprovalDto leaveRequestDto)
        {
            var command = new UpdateLeaveRequestCommand() { ChangeLeaveRequestApprovalDto = leaveRequestDto, Id = Id };
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand() { Id = id };
            var response = await _mediator.Send(command);
            return NoContent();
        }
    }
}

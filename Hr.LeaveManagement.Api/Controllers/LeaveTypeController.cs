using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
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
    public class LeaveTypeController : ControllerBase
    {

        public IMediator _mediator { get; }
        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int Id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest() { Id = Id });
            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> post([FromBody] CreateLeaveTypeDto createLeaveTypeDto)
        {
            var command = new CreateLeaveTypeCommand() { leaveTypeDto = createLeaveTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> put([FromBody] LeaveTypeDto leaveTypeDto)
        {
            var command = new UpdateLeaveTypeCommand() { leaveTypeDto = leaveTypeDto };
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand() { Id = id };
            var response = await _mediator.Send(command);
            return NoContent();
        }

    }
}

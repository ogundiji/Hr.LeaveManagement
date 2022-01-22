using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        public readonly IMediator _mediator;
        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveAllocationListRequest());
            return Ok(leaveRequests);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<LeaveAllocationDto>> Get(int Id)
        {
            var leaveType = await _mediator.Send(new GetLeaveAllocationDetailRequest() { Id = Id });
            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<ActionResult> post([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var command = new CreateLeaveAllocationCommand() { leaveAllocationDto = createLeaveAllocationDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> put(int Id, [FromBody] UpdateLeaveAllocationDto leaveAllocationDto)
        {
            var command = new UpdateLeaveAllocationCommand() { UpdateLeaveAllocationDto = leaveAllocationDto};
            var response = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllocationCommand() { Id = id };
            var response = await _mediator.Send(command);
            return NoContent();
        }
    }
}

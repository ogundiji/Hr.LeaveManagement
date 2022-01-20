using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandlers : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocation;
        private readonly IMapper _mapper;
        public GetLeaveAllocationListRequestHandlers(ILeaveAllocationRepository leaveAllocation, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.leaveAllocation = leaveAllocation;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var allocationList = await leaveAllocation.GetLeaveAllocationsWithDetails();
            return _mapper.Map<List<LeaveAllocationDto>>(allocationList);
        }
    }
}

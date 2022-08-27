using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;
using HR.LeaveManagement.Dormain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;

        public LeaveRequestService(IMapper mapper, IClient httpclient, ILocalStorageService localStorageService): base(httpclient, localStorageService)
        {
            this._localStorageService = localStorageService;
            this._mapper = mapper;
            this._httpclient = httpclient;
        }

      
        public async Task ApproveLeaveRequest(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var request = new ChangeLeaveRequestApprovalDto { Approved = approved, Id = id };
                await _client.ChangeapprovalAsync(id, request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto createLeaveRequest = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
                AddBearerToken();
                var apiResponse = await _client.LeaveRequestPOSTAsync(createLeaveRequest);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertedApiExceptions<int>(ex);
            }
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LeaveRequest> GetLeaveRequest(int id)
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestGETAsync(id);
            return _mapper.Map<LeaveRequest>(leaveRequest);
        }

        //public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
        //{
        //    AddBearerToken();
        //    var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: false);


        //    var model = new AdminLeaveRequestViewVM
        //    {
        //        TotalRequests = leaveRequests.Count,
        //        ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
        //        PendingRequests = leaveRequests.Count(q => q.Approved == null),
        //        RejectedRequests = leaveRequests.Count(q => q.Approved == false),
        //        LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        //    };
        //    return model;
        //}


    }
}

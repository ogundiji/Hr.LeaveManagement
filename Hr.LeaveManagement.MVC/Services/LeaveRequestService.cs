using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Services.Base;
using HR.LeaveManagement.Dormain;
using System;
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

        //public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest)
        //{
        //    try
        //    {
        //        var response = new Response<int>();
        //        CreateLeaveRequestDto createLeaveRequest = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
        //        AddBearerToken();
        //        var apiResponse = await _client.LeaveRequestPOSTAsync(createLeaveRequest);
        //        if (apiResponse.Success)
        //        {
        //            response.Data = apiResponse.Id;
        //            response.Success = true;
        //        }
        //        else
        //        {
        //            foreach (var error in apiResponse.Errors)
        //            {
        //                response.ValidationErrors += error + Environment.NewLine;
        //            }
        //        }
        //        return response;
        //    }
        //    catch (ApiException ex)
        //    {
        //        return ConvertApiExceptions<int>(ex);
        //    }
        //}

        public Task DeleteLeaveRequest(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<LeaveRequest> GetLeaveRequest(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Services.Base;
using System;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveAllocationService :BaseHttpService, ILeaveAllocationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _httpclient;
        public LeaveAllocationService(ILocalStorageService localStorageService, IClient httpclient):base(httpclient, localStorageService)
        {
            _localStorageService = localStorageService;
            _httpclient = httpclient;
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocation = new() { LeaveTypeId = leaveTypeId };
                AddBearerToken();
                var apiResponse = await _client.LeaveAllocationPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
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
    }
}

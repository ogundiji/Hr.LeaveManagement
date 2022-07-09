using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService,ILeaveTypeService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IMapper mapper;
        private readonly IClient _httpClient;

        public LeaveTypeService(ILocalStorageService _localStorageService,IMapper mapper,IClient _httpClient)
            :base(_httpClient,_localStorageService)
        {
            localStorageService = _localStorageService;
            this.mapper = mapper;
            this._httpClient = _httpClient;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createLeaveType = mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                var apiResponse = await _httpClient.LeaveTypePOSTAsync(createLeaveType);
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

        public async Task<Response<int>> DeleteLeaveType(int Id)
        {
            try
            {
                AddBearerToken();
                await _httpClient.LeaveTypeDELETEAsync(Id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertedApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVm> GetLeaveTypeDetails(int Id)
        {
            AddBearerToken();
            var leaveType = await _httpClient.LeaveTypeGETAsync(Id);
            return mapper.Map<LeaveTypeVm>(leaveType);
        }

        public async Task<List<LeaveTypeVm>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveTypes = await _httpClient.LeaveTypeAllAsync();
            return mapper.Map<List<LeaveTypeVm>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(LeaveTypeVm leaveType)
        {
            try
            {
                LeaveTypeDto leaveTypeDto = mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await _httpClient.LeaveTypePUTAsync(leaveTypeDto);
                  
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertedApiExceptions<int>(ex);
            }
        }
    }
}

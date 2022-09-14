using Hr.LeaveManagement.MVC.Contracts;
using System.Net.Http.Headers;

namespace Hr.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorage;
        protected IClient _client;
        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _client = client;
        }

        protected Response<Guid> ConvertedApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "validations Error has occured", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid> { Message = "The requested item could not be found", Success = false };
            }
            else
            {
                return new Response<Guid>() { Message = "something went wrong please try again", Success = false };
            }
        }

        protected void AddBearerToken()
        {
            
            if (_localStorage.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", _localStorage.GetSTorageValue<string>("token"));
        }
    }
}

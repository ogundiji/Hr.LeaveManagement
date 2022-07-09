using System.Net.Http;

namespace Hr.LeaveManagement.MVC.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

    }
}

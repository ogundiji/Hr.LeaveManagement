using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationServices
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(string firstName, string lastName, string userName, string email, string password);
        Task Logout();
    }
}

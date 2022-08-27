using Hr.LeaveManagement.MVC.Models;
using System.Threading.Tasks;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface IAuthenticationServices
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVm registration);
        Task Logout();
    }
}

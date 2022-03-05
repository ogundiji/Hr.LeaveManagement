using System.Collections.Generic;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILocalStorageService
    {
        void clearStorage(List<string> keys);
        bool Exists(string key);
        T GetSTorageValue<T>(string key);
        void setStorageValue<T>(string key, T value);

    }
}

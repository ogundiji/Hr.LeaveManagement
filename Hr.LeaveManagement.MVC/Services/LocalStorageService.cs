using Hanssens.Net;
using Hr.LeaveManagement.MVC.Contracts;
using System.Collections.Generic;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage _storage;
        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration
            {
                AutoLoad=true,
                AutoSave=true,
                Filename="HR.LEAVEMGMT"
            };
            _storage= new LocalStorage(config);
        }
        public void clearStorage(List<string> keys)
        {
           foreach(var key in keys)
            {
                _storage.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            return _storage.Exists(key);
        }

        public T GetSTorageValue<T>(string key)
        {
            return _storage.Get<T>(key);
        }

        public void setStorageValue<T>(string key, T value)
        {
            _storage.Store(key, value);
            _storage.Persist();
        }
    }
}

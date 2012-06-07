using System;

namespace DevBridge.Templates.WebProject.ServiceContracts
{
    public delegate T GetCacheObjectDelegate<out T>();

    public interface ICachingService
    {        
        TimeSpan DefaultTimeout { get; }
        void Set<T>(string key, T obj);
        void Set<T>(string key, T obj, TimeSpan expiration);
        T Get<T>(string key);
        T Get<T>(string key, GetCacheObjectDelegate<T> getCacheObjectDelegate);
        T Get<T>(string key, TimeSpan expiration, GetCacheObjectDelegate<T> getCacheObjectDelegate);
        void Remove(string key);
    }
}

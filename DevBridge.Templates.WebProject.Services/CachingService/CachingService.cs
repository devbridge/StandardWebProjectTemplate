using System;
using System.Web;
using System.Web.Caching;
using DevBridge.Templates.WebProject.ServiceContracts;

namespace DevBridge.Templates.WebProject.Services
{
    public class CachingService : ICachingService
    {
        public TimeSpan DefaultTimeout { get; private set; }

        private readonly CachingServiceConfiguration config;

        public CachingService(IConfigurationLoaderService configurationLoaderService)
        {
            config = configurationLoaderService.LoadConfig<CachingServiceConfiguration>();
            DefaultTimeout = config.DefaultTimeout;
        }

        public CachingService(TimeSpan defaultTimeout)
        {
            DefaultTimeout = defaultTimeout;
        }

        public CachingService()
        {
            DefaultTimeout = TimeSpan.FromMinutes(10);
        }

        public void Set<T>(string key, T obj)
        {
            Set(key, obj, DefaultTimeout);
        }

        public void Set<T>(string key, T obj, TimeSpan expiration)
        {
            Set(key, obj, expiration, null);
        }

        internal void Set<T>(string key, T obj, TimeSpan expiration, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            if (obj == null)
            {
                Remove(key);
            }
            else
            {
                HttpRuntime.Cache.Add(key.ToUpperInvariant(), obj, null, DateTime.Now.Add(expiration),
                                      Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable,
                                      cacheItemRemovedCallback);                                      
            }
        }

        public T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache[key.ToUpperInvariant()];
        }

        public T Get<T>(string key, TimeSpan expiration, GetCacheObjectDelegate<T> getCacheObjectDelegate)
        {
            return Get(key, expiration, getCacheObjectDelegate, null);
        }        

        public T Get<T>(string key, GetCacheObjectDelegate<T> getCacheObjectDelegate)
        {
            return Get(key, DefaultTimeout, getCacheObjectDelegate, null);
        }

        internal T Get<T>(string key, TimeSpan expiration, GetCacheObjectDelegate<T> getCacheObjectDelegate, CacheItemRemovedCallback cacheItemRemovedCallback)
        {
            T obj = Get<T>(key);
            if (obj == null)
            {
                obj = getCacheObjectDelegate();
                Set(key, obj, expiration, cacheItemRemovedCallback);
            }
            return obj;
        }

        public void Remove(string key)
        {
            if (HttpRuntime.Cache[key.ToUpperInvariant()] != null)
            {
                HttpRuntime.Cache.Remove(key.ToUpperInvariant());
            }
        }
    }
}

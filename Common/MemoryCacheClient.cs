using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SampleProject.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core;
using ServiceStack.Caching;

namespace SampleProject.Common
{
    public class MemoryCacheClient : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly ILogger _logger;

        public ICacheClient Client => throw new NotImplementedException();

        public MemoryCacheClient(ILogger<RedisCacheClient> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public bool Add<T>(string key, T value, int expiresInMinutes = 60)
        {
            key = ModifyKey(key);

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            try
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(TimeSpan.FromMinutes(expiresInMinutes));
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                _memoryCache.Set(key, value, options);
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:AddWithExpiry:key={key},Error={ex.Message}");
                return false;
            }

            return true;
        }

        public bool Add<T>(string key, T value)
        {
            key = ModifyKey(key);

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);
                options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

                _memoryCache.Set(key, value, options);
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:AddWithoutExpiry:key={key},Error={ex.Message}");
                return false;
            }

            return true;
        }

        public T Get<T>(string key) where T : class, new()
        {
            key = ModifyKey(key);

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                if (_memoryCache.TryGetValue(key, out T result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:GetWithT:key={key},Error={ex.Message}");
            }

            return null;
        }

        public string Get(string key)
        {
            key = ModifyKey(key);

            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            key = ModifyKey(key);

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                _memoryCache.Remove(key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:Remove:key={key},Error={ex.Message}");
                return false;
            }

            return true;
        }

        public bool RemoveWithPattern(string pattern)
        {
            pattern = ModifyKey(pattern);

            return true;
        }
        /// <summary>
        /// Flush all databases
        /// </summary>
        /// <returns></returns>
        public bool Flush()
        {
            try
            {
                if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
                {
                    _resetCacheToken.Cancel();
                    _resetCacheToken.Dispose();
                }

                _resetCacheToken = new CancellationTokenSource();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:Flush,Error={ex.Message}");
                return false;
            }
        }

        private string ModifyKey(string key)
        {
            return AppSettings.BuildMode.ToString().ToLower() + $"-{key}";
        }

        private DateTimeOffset MinutesToOffset(int minutes)
        {
            return (new DateTimeOffset(DateTime.UtcNow)).ToOffset(TimeSpan.FromMinutes(minutes));
        }
    }

}

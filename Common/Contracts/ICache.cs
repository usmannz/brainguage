using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Caching;
using StackExchange.Redis.Extensions.Core;

namespace SampleProject.Common.Contracts
{
    public interface ICache
    {
        ICacheClient Client { get; }

        /// <summary>
        /// Add value to cache
        /// </summary>
        /// <returns></returns>
        bool Add<T>(string key, T value, int expiresInMinutes = 60);

        /// <summary>
        /// Get value by key
        /// </summary>
        /// <returns></returns>
        T Get<T>(string key) where T : class, new();

        /// <summary>
        /// Get value by key
        /// </summary>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// remove key from cache
        /// </summary>
        /// <returns></returns>
        bool Remove(string key);
        /// <summary>
        /// remove keys with matching pattern
        /// </summary>
        /// <returns></returns>
        bool RemoveWithPattern(string pattern);
        /// <summary>
        /// Add value to cache
        /// </summary>
        /// <returns></returns>
        bool Add<T>(string key, T value);

        /// <summary>
        /// Flush all databases
        /// </summary>
        /// <returns></returns>
        bool Flush();
    }

}

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SampleProject.Common.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis.Extensions.Core;
using ServiceStack.Caching;

namespace SampleProject.Common
{
    public class RedisCacheClient : ICache
    {
        private readonly ILogger _logger;

        public ICacheClient Client => throw new NotImplementedException();
        private readonly int DbIndex = (int)AppSettings.BuildMode;
        private readonly IDatabase _database;
        private readonly IServer _server;
        public RedisCacheClient(ILogger<RedisCacheClient> logger)
        {
            _logger = logger;
            _database = Connection.GetDatabase(DbIndex);
            _server = Connection.GetServer(Connection.GetEndPoints().First());
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(AppSettings.CacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public bool Add<T>(string key, T value, int expiresInMinutes = 60)
        {
            key = ModifyKey(key);

            var database = GetDatabase();

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
                var serialised = JsonConvert.SerializeObject(value);
                database.StringSetAsync(key, serialised).ConfigureAwait(false);
                database.KeyExpireAsync(key, TimeSpan.FromMinutes(expiresInMinutes)).ConfigureAwait(false);
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

            var database = GetDatabase();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            // if (value == null)
            // {
            //     throw new ArgumentNullException(nameof(value));
            // }

            try
            {
                string json = JsonConvert.SerializeObject(value, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                database.StringSetAsync(key, json).ConfigureAwait(false);
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

            var database = GetDatabase();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            RedisValue result = RedisValue.Null;
            try
            {
                result = database.StringGetAsync(key).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:GetWithT:key={key},Error={ex.Message}");
            }

            if (result.IsNullOrEmpty)
            {
                return null;
            }

            var deserialised = JsonConvert.DeserializeObject<T>(result);
            return deserialised;
        }

        public string Get(string key)
        {
            key = ModifyKey(key);

            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            key = ModifyKey(key);

            var database = GetDatabase();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            try
            {
                database.KeyDeleteAsync(key).ConfigureAwait(false);
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

            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            try
            {
                var keys = _server.Keys(database: DbIndex, pattern: pattern + "*", pageSize: 2000).ToArray();

                _database.KeyDeleteAsync(keys);
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:RemoveWithPattern:key={pattern},Error={ex.Message}");
                return false;
            }

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
                var endpoints = Connection.GetEndPoints();

                var server = Connection.GetServer(endpoints.First());

                //server.FlushAllDatabases();

                server.FlushDatabaseAsync((int)AppSettings.BuildMode);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"<<<<<RedisCacheManager:Flush,Error={ex.Message}");
                return false;
            }
        }

        private IDatabase GetDatabase()
        {
            return Connection.GetDatabase((int)AppSettings.BuildMode);
        }

        private string ModifyKey(string key)
        {
            return AppSettings.BuildMode.ToString().ToLower() + $"-{key}";
        }
    }

}

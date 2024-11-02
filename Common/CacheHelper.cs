using Common;
using FRCSPreparationPortal.Common.Contracts;
using FRCSPreparationPortal.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Common
{
    public class CacheHelper
    {
        private readonly ICache _cache;

        public CacheHelper(ICache cache)
        {
            _cache = cache;
        }

        public bool AddUsersAll(IEnumerable<Users> users) => _cache.Add(CacheKeys.USERS_ALL, users);
        public bool DeleteUsersAll() => _cache.Remove(CacheKeys.USERS_ALL);
        public List<Users> GetUsersAll() => _cache.Get<List<Users>>(CacheKeys.USERS_ALL);

        public void Flush()
        {
            _cache.Flush();
        }
    }
}

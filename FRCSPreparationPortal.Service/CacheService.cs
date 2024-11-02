using FRCSPreparationPortal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service
{
    public class CacheService
    {
        //private readonly IUserService _userService;
        //private readonly IProjectService _projectService;
        private readonly CacheHelper _cacheHelper;

        public CacheService(
            //IUserService userService,
            //IProjectService projectService,
            CacheHelper cacheHelper
            )
        {
            _cacheHelper = cacheHelper;
            //_userService = userService;
            //_projectService = projectService;

        }

        /// <summary>
        /// Load Application Cache
        /// </summary>
        public async Task LoadApplicationCache()
        {
            //await _userService.GetAllUsers();

        }

        public void RegisterNightlyJobs()
        {
            //_cacheHelper.DeleteUsersAll();
            //_userService.GetAllUsers();
        }

        public async Task RegisterWeeklyJobs()
        {
            //await _projectService.CalculateProjectLastPeriodCompletion();
            //await _projectService.CalculateProjectPhaseLastPeriodCompletion();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FRCSPreparationPortal.API;
using FRCSPreparationPortal.API.Controllers;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Service.Contracts;

namespace FRCSPreparationPortal.Controllers
{
    [Authorize, Route("api/preptest")]
    public class PrepTestController : BaseController
    {
        private readonly ILogger<PrepTestController> _logger;
        private IPrepTestService _prepTestService;

        public PrepTestController(ILogger<PrepTestController> logger, IPrepTestService prepTestService)
        {
            _logger = logger;
            _prepTestService = prepTestService;
        }

         [Authorize(Roles = "Admin,User"),HttpGet("GeneratePrepTest/{userId}")]        
        public async Task<IActionResult> GeneratePrepTest(int userId)
        {
            if(userId == 0)
            {
                userId = UserId;
            }

            return Ok(await _prepTestService.GeneratePrepTest(userId));
        }

         [Authorize(Roles = "Admin,User"),HttpPost("SavePrepTestResponse/{isSubmitted}/{timeLeft}")]        
        public async Task<IActionResult> SavePrepTestResponse([FromBody] List<ViewPrepTestListing> response,bool isSubmitted,int timeLeft)
        {
            
            return Ok(await _prepTestService.SavePrepTestResponse(response, isSubmitted, timeLeft,UserId));
        }

          [Authorize(Roles = "User"),HttpPost("GetAllPrepTests")] 
        public async Task <ApiResponse<ViewModelPrepTestConfigListing>> GetAllPrepTests([FromBody] Pager pagination)
        {
            return await _prepTestService.GetAllPrepTests(pagination);
        }

           [Authorize(Roles = "User"),HttpPost("SavePrepTestConfig")]        
        public async Task<IActionResult> SavePrepTestConfig([FromBody] SavePrepTestConfig config)
        {
            
            return Ok(await _prepTestService.SavePrepTestConfig(config,UserId));
        }

         [Authorize(Roles = "User"),HttpGet("GetPrepTestById/{prepTestConfigId}")]        
        public async Task<IActionResult> GetPrepTestById(int prepTestConfigId)
        {

            return Ok(await _prepTestService.GetPrepTestById(prepTestConfigId,UserId));
        }

    }
}

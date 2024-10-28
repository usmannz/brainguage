using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API;
using SampleProject.API.Controllers;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Service.Contracts;

namespace SampleProject.Controllers
{
    [Authorize, Route("api/demotest")]
    public class DemoTestController : BaseController
    {
        private readonly ILogger<DemoTestController> _logger;
        private IDemoTestService _demoTestService;

        public DemoTestController(ILogger<DemoTestController> logger, IDemoTestService demoTestService)
        {
            _logger = logger;
            _demoTestService = demoTestService;
        }

         [Authorize(Roles = "Admin,User"),HttpGet("GenerateDemoTest/{userId}")]        
        public async Task<IActionResult> GenerateDemoTest(int userId)
        {
            if(userId == 0)
            {
                userId = UserId;
            }

            return Ok(await _demoTestService.GenerateDemoTest(userId));
        }

         [Authorize(Roles = "Admin,User"),HttpPost("SaveDemoTestResponse")]        
        public async Task<IActionResult> SaveDemoTestResponse([FromBody] List<ViewDemoTestListing> response)
        {
            
            return Ok(await _demoTestService.SaveDemoTestResponse(response,UserId));
        }
    }
}

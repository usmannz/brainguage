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
    [Authorize, Route("api/mocktest")]
    public class MockTestController : BaseController
    {
        private readonly ILogger<MockTestController> _logger;
        private IMockTestService _mockTestService;

        public MockTestController(ILogger<MockTestController> logger, IMockTestService mockTestService)
        {
            _logger = logger;
            _mockTestService = mockTestService;
        }

         [Authorize(Roles = "Admin,User"),HttpGet("GenerateMockTest/{userId}")]        
        public async Task<IActionResult> GenerateMockTest(int userId)
        {
            if(userId == 0)
            {
                userId = UserId;
            }

            return Ok(await _mockTestService.GenerateMockTest(userId));
        }

         [Authorize(Roles = "Admin,User"),HttpPost("SaveMockTestResponse")]        
        public async Task<IActionResult> SaveMockTestResponse([FromBody] List<ViewMockTestListing> response)
        {
            
            return Ok(await _mockTestService.SaveMockTestResponse(response,UserId));
        }
    }
}

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
    [Authorize, Route("api/quiz")]
    public class QuizController : BaseController
    {
        private readonly ILogger<QuizController> _logger;
        private IQuizService _quizService;

        public QuizController(ILogger<QuizController> logger, IQuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }

         [Authorize(Roles = "Admin,User"),HttpGet("GenerateQuiz/{userId}")]        
        public async Task<IActionResult> GenerateQuiz(int userId)
        {
            if(userId == 0)
            {
                userId = UserId;
            }

            return Ok(await _quizService.GenerateQuiz(userId));
        }

         [Authorize(Roles = "Admin,User"),HttpPost("SaveQuizResponse")]        
        public async Task<IActionResult> SaveQuizResponse([FromBody] List<ViewUserQuizListing> response)
        {
            
            return Ok(await _quizService.SaveQuizResponse(response,UserId));
        }
    }
}

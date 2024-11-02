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

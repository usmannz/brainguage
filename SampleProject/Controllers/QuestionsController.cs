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
    [ApiController]
    [Authorize, Route("api/questions")]
    public class QuestionsController : BaseController
    {
        private readonly ILogger<QuestionsController> _logger;
        private IQuestionService _questionService;

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionService questionService)
        {
            _logger = logger;
            _questionService = questionService;
        }

        [Authorize(Roles = "Admin")]
        public async Task <ApiResponse<ViewModelQuestionListing>> GetAllQuestions([FromBody] Pager pagination)
        {
            return await _questionService.GetAllQuestions(pagination);
        }

          [Authorize(Roles = "Admin"),HttpPost("SaveQuestion")]        
        public async Task<IActionResult> SaveQuestion([FromBody] Questions question)
        {
            
            return Ok(await _questionService.SaveQuestion(question,UserId));
        }
        [Authorize(Roles = "Admin"), HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuuestion(int questionId)
        {
            int deletedBy = this.User.GetUserId();
            return Ok(await _questionService.DeleteQuestion(questionId, deletedBy));
        }
    }
}

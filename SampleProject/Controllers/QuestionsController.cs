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

        [Authorize(Roles = "Admin"),HttpPost("GetAllQuestions")] 
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
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            int deletedBy = this.User.GetUserId();
            return Ok(await _questionService.DeleteQuestion(questionId, deletedBy));
        }
        [Authorize(Roles = "Admin,User"),HttpPost("GetAllUsersQuestions/{userId}")]
        public async Task<ApiResponse<ViewModelUserQuestionListing>> GetAllUsersQuestions([FromBody] Pager pagination,int userId)
        {
            if (userId == 0)
                userId = UserId;
            return await _questionService.GetAllUsersQuestions(pagination, userId);
        }

         [Authorize(Roles = "Admin"),HttpPost("AssignQuestions")]        
        public async Task<IActionResult> AssignQestions([FromBody] List<QuestionsAssignment> question)
        {
            
            return Ok(await _questionService.AssignQestions(question,UserId));
        }
    }
}

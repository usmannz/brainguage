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
        public async Task<IActionResult> SaveQuestion([FromForm] QuestionDto questionDto)

        {
            var question = new Questions
            {
                Id = questionDto.Id,
                Question = questionDto.Question, // Assuming Question property is the same in both
                Description = questionDto.Description,
                Option1 = questionDto.Option1,
                Option2 = questionDto.Option2,
                Option3 = questionDto.Option3,
                Option4 = questionDto.Option4,
                Option5 = questionDto.Option5,
                isMockExam = questionDto.IsMockExam,
                isPrepExam =questionDto.IsPrepExam,
                IsDemo = questionDto.IsDemo,
                CategoriesId = questionDto.CategoriesId,
                CorrectAnswer = questionDto.CorrectAnswer,
            };
            return Ok(await _questionService.SaveQuestion(question, questionDto.File, UserId));
        }
        [Authorize(Roles = "Admin"), HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            int deletedBy = this.User.GetUserId();
            return Ok(await _questionService.DeleteQuestion(questionId, deletedBy));
        }

    }
}

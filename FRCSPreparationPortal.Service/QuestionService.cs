using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Contracts;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Contracts;
using FRCSPreparationPortal.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> _logger;
        private readonly IQuestionRepository _questionRepository;
        public QuestionService(
            ILogger<QuestionService> logger,
            IQuestionRepository questionRepository
        )
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<ViewModelQuestionListing>> GetAllQuestions(Pager pagination)
        {
             ViewModelQuestionListing listQuestions = new ViewModelQuestionListing();
         try
            {
                 listQuestions = await _questionRepository.GetAllQuestions(pagination);
                 return new ApiResponse<ViewModelQuestionListing>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listQuestions // listUsers may be an empty list if no users are found
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewModelQuestionListing>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new ViewModelQuestionListing() // Return an empty list in case of error
                };
            }
        }

                public async Task<ApiResponse<int>> SaveQuestion(Questions question, IFormFile File, int userId)
        {
            question.Question = question.Question.Trim();
            question.Description =question.Description.Trim();
            question.Option1 =question.Option1.Trim();
            question.Option2 =question.Option2.Trim();
            question.Option3 =question.Option3.Trim();
            question.Option4 =question.Option4.Trim();
            question.Option5 =question.Option5.Trim();
            if (question.Id > 0)
            {
                question.UpdatedBy = userId;
                question.UpdateStamp = DateTime.UtcNow;
            }
            else
            {
                question.CreatedBy = userId;
                question.CreateStamp = DateTime.UtcNow;

            }
             try
            {
            var licenseid = await _questionRepository.SaveQuestion(question, File);
              return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = licenseid // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = 0 // Return an empty list in case of error
                };
            }

        }

        public async Task<ApiResponse<int>> DeleteQuestion(int questionId, int deletedBy)
        {
            try{
            var id = await _questionRepository.DeleteQuestion(questionId, deletedBy);
               return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = id // listUsers may be an empty list if no users are found
                };
            }
              catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = 0 // Return an empty list in case of error
                };
            }
        }        
    }
}

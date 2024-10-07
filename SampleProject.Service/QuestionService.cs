using Hangfire;
using Microsoft.Extensions.Logging;
using SampleProject.Common;
using SampleProject.Common.Contracts;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service
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

                public async Task<ApiResponse<int>> SaveQuestion(Questions question, int userId)
        {
            question.Question = question.Question.Trim();
            question.Answer =question.Answer.Trim();

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
            var licenseid = await _questionRepository.SaveQuestion(question);
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

        public async Task<ApiResponse<ViewModelUserQuestionListing>> GetAllUsersQuestions(Pager pagination, int userId)
        {
            ViewModelUserQuestionListing listQuestions = new ViewModelUserQuestionListing();
            try
            {
                listQuestions = await _questionRepository.GetAllUsersQuestions(pagination, userId);
                return new ApiResponse<ViewModelUserQuestionListing>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listQuestions // listUsers may be an empty list if no users are found
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewModelUserQuestionListing>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new ViewModelUserQuestionListing() // Return an empty list in case of error
                };
            }
        }

          public async Task<ApiResponse<int>> AssignQestions(List<QuestionsAssignment> question, int userId)
        {

          
             try
            {
            var licenseid = await _questionRepository.AssignQestions(question,userId);
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
        

    }
}

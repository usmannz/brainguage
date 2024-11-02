using Hangfire;
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
    public class QuizService : IQuizService
    {
        private readonly ILogger<QuizService> _logger;
        private readonly IQuizRepository _quizRepository;
        public QuizService(
            ILogger<QuizService> logger,
            IQuizRepository quizRepository
        )
        {
            _quizRepository = quizRepository;
            _logger = logger;
        }

          public async Task<ApiResponse<List<ViewUserQuizListing>>> GenerateQuiz(int userId)
        {      
             try
            {
            var listQuiz = await _quizRepository.GenerateQuiz(userId);
              return new ApiResponse<List<ViewUserQuizListing>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listQuiz // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<List<ViewUserQuizListing>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<ViewUserQuizListing>() // Return an empty list in case of error
                };
            }

        }   

    public async Task<ApiResponse<int>> SaveQuizResponse(List<ViewUserQuizListing> response, int userId)
        {
             try
            {
            var licenseid = await _quizRepository.SaveQuizResponse(response,userId);
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

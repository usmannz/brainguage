using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface IQuizService
    {
     Task<ApiResponse<List<ViewUserQuizListing>>> GenerateQuiz(int userId);
    Task<ApiResponse<int>> SaveQuizResponse(List<ViewUserQuizListing> response, int userId);

    }
}

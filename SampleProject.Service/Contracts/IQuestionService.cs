using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface IQuestionService
    {
    Task<ApiResponse<ViewModelQuestionListing>> GetAllQuestions(Pager pagination);
    Task<ApiResponse<int>> SaveQuestion(Questions question, int userId);
    Task<ApiResponse<int>> DeleteQuestion(int questionId, int deletedBy);
    Task<ApiResponse<ViewModelUserQuestionListing>> GetAllUsersQuestions(Pager pagination, int userId);
    Task<ApiResponse<int>> AssignQestions(List<QuestionsAssignment> question, int userId);

    }
}

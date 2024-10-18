using Microsoft.AspNetCore.Http;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository.Contracts
{
    public interface IQuestionRepository : IRepository<Questions>
    {
    Task<ViewModelQuestionListing> GetAllQuestions(Pager pagination);
    Task<int> SaveQuestion(Questions question, IFormFile File);
    Task<int> DeleteQuestion(int questionId, int deletedBy);
     Task<ViewModelUserQuestionListing> GetAllUsersQuestions(Pager pagination, int questionId);
   Task<int> AssignQestions(List<QuestionsAssignment> question, int userId);


    }
}

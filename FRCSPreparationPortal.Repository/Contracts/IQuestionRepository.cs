using Microsoft.AspNetCore.Http;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Contracts
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

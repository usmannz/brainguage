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
    public interface IQuizRepository : IRepository<UserQuiz>
    {
    Task<List<ViewUserQuizListing>> GenerateQuiz(int userId);
    Task<int> SaveQuizResponse(List<ViewUserQuizListing> response,int userId);
    }
}

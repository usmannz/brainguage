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
    public interface IQuizRepository : IRepository<UserQuiz>
    {
    Task<List<ViewUserQuizListing>> GenerateQuiz(int userId);
    Task<int> SaveQuizResponse(List<ViewUserQuizListing> response,int userId);
    }
}

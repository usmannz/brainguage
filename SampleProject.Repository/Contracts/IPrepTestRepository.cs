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
    public interface IPrepTestRepository : IRepository<PrepTest>
    {
    Task<List<ViewPrepTestListing>> GeneratePrepTest(int userId);
    Task<int> SavePrepTestResponse(List<ViewPrepTestListing> response,int userId);
    Task<ViewModelPrepTestConfigListing> GetAllPrepTests(Pager pagination);
    Task<int> SavePrepTestConfig(SavePrepTestConfig config,int userId);
    Task<List<ViewPrepTestListing>> GetPrepTestById(int prepTestId, int userId);


    }
}

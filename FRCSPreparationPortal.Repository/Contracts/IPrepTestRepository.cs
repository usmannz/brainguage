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
    public interface IPrepTestRepository : IRepository<PrepTest>
    {
    Task<List<ViewPrepTestListing>> GeneratePrepTest(int userId);
    Task<int> SavePrepTestResponse(List<ViewPrepTestListing> response, bool isSubmitted, int timeLeft, int userId);
    Task<ViewModelPrepTestConfigListing> GetAllPrepTests(Pager pagination);
    Task<int> SavePrepTestConfig(SavePrepTestConfig config,int userId);
    Task<List<ViewPrepTestListing>> GetPrepTestById(int prepTestId, int userId);


    }
}

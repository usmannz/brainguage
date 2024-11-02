using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service.Contracts
{
    public interface IPrepTestService
    {
     Task<ApiResponse<List<ViewPrepTestListing>>> GeneratePrepTest(int userId);
    Task<ApiResponse<int>> SavePrepTestResponse(List<ViewPrepTestListing> response, int userId);
    Task<ApiResponse<ViewModelPrepTestConfigListing>> GetAllPrepTests(Pager pagination);
        Task<ApiResponse<int>> SavePrepTestConfig(SavePrepTestConfig config, int userId);
     Task<ApiResponse<List<ViewPrepTestListing>>> GetPrepTestById(int prepTestConfigId, int userId);

    }
}

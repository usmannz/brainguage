using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface IMockTestService
    {
     Task<ApiResponse<List<ViewMockTestListing>>> GenerateMockTest(int userId);
    Task<ApiResponse<int>> SaveMockTestResponse(List<ViewMockTestListing> response, int userId);
    }
}

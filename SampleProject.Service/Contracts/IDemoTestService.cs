using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface IDemoTestService
    {
     Task<ApiResponse<List<ViewDemoTestListing>>> GenerateDemoTest(int userId);
    Task<ApiResponse<int>> SaveDemoTestResponse(List<ViewDemoTestListing> response, int userId);
    }
}

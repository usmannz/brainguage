using Hangfire;
using Microsoft.Extensions.Logging;
using SampleProject.Common;
using SampleProject.Common.Contracts;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service
{
    public class DemoTestService : IDemoTestService
    {
        private readonly ILogger<DemoTestService> _logger;
        private readonly IDemoTestRepository _demoTestRepository;
        public DemoTestService(
            ILogger<DemoTestService> logger,
            IDemoTestRepository demoTestRepository
        )
        {
            _demoTestRepository = demoTestRepository;
            _logger = logger;
        }

          public async Task<ApiResponse<List<ViewDemoTestListing>>> GenerateDemoTest(int userId)
        {      
             try
            {
            var listDemoTest = await _demoTestRepository.GenerateDemoTest(userId);
              return new ApiResponse<List<ViewDemoTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listDemoTest // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<List<ViewDemoTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<ViewDemoTestListing>() // Return an empty list in case of error
                };
            }

        }   

    public async Task<ApiResponse<int>> SaveDemoTestResponse(List<ViewDemoTestListing> response, int userId)
        {
             try
            {
            var licenseid = await _demoTestRepository.SaveDemoTestResponse(response,userId);
              return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = licenseid // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = 0 // Return an empty list in case of error
                };
            }

        }

    }
}

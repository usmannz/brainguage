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
    public class MockTestService : IMockTestService
    {
        private readonly ILogger<MockTestService> _logger;
        private readonly IMockTestRepository _mockTestRepository;
        public MockTestService(
            ILogger<MockTestService> logger,
            IMockTestRepository mockTestRepository
        )
        {
            _mockTestRepository = mockTestRepository;
            _logger = logger;
        }

          public async Task<ApiResponse<List<ViewMockTestListing>>> GenerateMockTest(int userId)
        {      
             try
            {
            var listMockTest = await _mockTestRepository.GenerateMockTest(userId);
              return new ApiResponse<List<ViewMockTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listMockTest // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<List<ViewMockTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<ViewMockTestListing>() // Return an empty list in case of error
                };
            }

        }   

    public async Task<ApiResponse<int>> SaveMockTestResponse(List<ViewMockTestListing> response, int userId)
        {
             try
            {
            var licenseid = await _mockTestRepository.SaveMockTestResponse(response,userId);
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

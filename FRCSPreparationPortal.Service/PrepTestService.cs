using Hangfire;
using Microsoft.Extensions.Logging;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Contracts;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Contracts;
using FRCSPreparationPortal.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service
{
    public class PrepTestService : IPrepTestService
    {
        private readonly ILogger<PrepTestService> _logger;
        private readonly IPrepTestRepository _prepTestRepository;
        public PrepTestService(
            ILogger<PrepTestService> logger,
            IPrepTestRepository prepTestRepository
        )
        {
            _prepTestRepository = prepTestRepository;
            _logger = logger;
        }

          public async Task<ApiResponse<List<ViewPrepTestListing>>> GeneratePrepTest(int userId)
        {      
             try
            {
            var listPrepTest = await _prepTestRepository.GeneratePrepTest(userId);
              return new ApiResponse<List<ViewPrepTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listPrepTest // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<List<ViewPrepTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<ViewPrepTestListing>() // Return an empty list in case of error
                };
            }

        }   

    public async Task<ApiResponse<int>> SavePrepTestResponse(List<ViewPrepTestListing> response, int userId)
        {
             try
            {
            var licenseid = await _prepTestRepository.SavePrepTestResponse(response,userId);
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

         public async Task<ApiResponse<ViewModelPrepTestConfigListing>> GetAllPrepTests(Pager pagination)
        {
             ViewModelPrepTestConfigListing listPrepTestConfig = new ViewModelPrepTestConfigListing();
         try
            {
                 listPrepTestConfig = await _prepTestRepository.GetAllPrepTests(pagination);
                 return new ApiResponse<ViewModelPrepTestConfigListing>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listPrepTestConfig // listUsers may be an empty list if no users are found
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewModelPrepTestConfigListing>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new ViewModelPrepTestConfigListing() // Return an empty list in case of error
                };
            }
        }

              public async Task<ApiResponse<int>> SavePrepTestConfig(SavePrepTestConfig config, int userId)
        {
            config.Name = config.Name.Trim();

            if (config.Id > 0)
            {
                config.UpdatedBy = userId;
                config.UpdateStamp = DateTime.UtcNow;
            }
            else
            {
                config.CreatedBy = userId;
                config.CreateStamp = DateTime.UtcNow;

            }
             try
            {
            var licenseid = await _prepTestRepository.SavePrepTestConfig(config,userId);
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

          public async Task<ApiResponse<List<ViewPrepTestListing>>> GetPrepTestById(int prepTestConfigId, int userId)
        {      
             try
            {
            var listPrepTest = await _prepTestRepository.GetPrepTestById(prepTestConfigId, userId);
              return new ApiResponse<List<ViewPrepTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listPrepTest // listUsers may be an empty list if no users are found
                };
            }
               catch (Exception ex)
            {
                return new ApiResponse<List<ViewPrepTestListing>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<ViewPrepTestListing>() // Return an empty list in case of error
                };
            }

        }   


    }
}

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
using FRCSPreparationPortal.Repository;

namespace FRCSPreparationPortal.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(
            ILogger<CategoryService> logger,
            ICategoryRepository categoryRepository
        )
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<ViewModelCategoriesListing>> GetAllCategories(Pager pagination)
        {
             ViewModelCategoriesListing listCategories = new ViewModelCategoriesListing();
         try
            {
                 listCategories = await _categoryRepository.GetAllCategories(pagination);
                 return new ApiResponse<ViewModelCategoriesListing>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listCategories // listUsers may be an empty list if no users are found
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ViewModelCategoriesListing>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new ViewModelCategoriesListing() // Return an empty list in case of error
                };
            }
        }

                public async Task<ApiResponse<int>> SaveCategory(Categories category, int userId)
        {
            category.Name = category.Name.Trim();

            if (category.Id > 0)
            {
                category.UpdatedBy = userId;
                category.UpdateStamp = DateTime.UtcNow;
            }
            else
            {
                category.CreatedBy = userId;
                category.CreateStamp = DateTime.UtcNow;

            }
             try
            {
            var licenseid = await _categoryRepository.SaveCategory(category);
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

        public async Task<ApiResponse<int>> DeleteCategory(int categoryId, int deletedBy)
        {
            try{
            var id = await _categoryRepository.DeleteCategory(categoryId, deletedBy);
               return new ApiResponse<int>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = id // listUsers may be an empty list if no users are found
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

        public async Task<ApiResponse<List<Categories>>> GetAllDropDownCategories()
        {
                try
                {
                var listCategories = await _categoryRepository.GetAllDropDownCategories();
                return new ApiResponse<List<Categories>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listCategories
                };
                }
                catch (Exception ex)
            {
                return new ApiResponse<List<Categories>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<Categories>() // Return an empty list in case of error
                };
            }

            
            //var listUsers = await _userRepository.GetAllUsers();
            //return await Task.FromResult<ApiResponse<List<Users>>>(listUsers);
        }

        public async Task<ApiResponse<List<Products>>> GetAllProducts()
        {
            try
            {
                var listProducts = await _categoryRepository.GetAllProducts();
                return new ApiResponse<List<Products>>
                {
                    Status = new ApiResponseStatus { Code = 200, Message = "Success" },
                    Data = listProducts
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Products>>
                {
                    Status = new ApiResponseStatus { Code = 500, Message = "Internal Server Error" },
                    Data = new List<Products>() // Return an empty list in case of error
                };
            }
        }

        public async Task<Products> GetProductById(int productId)
        {
            return await _categoryRepository.GetProductById(productId);
        }
    }
}

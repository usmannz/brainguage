using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Service.Contracts
{
    public interface ICategoryService
    {
    Task<ApiResponse<ViewModelCategoriesListing>> GetAllCategories(Pager pagination);
    Task<ApiResponse<int>> SaveCategory(Categories category, int userId);
    Task<ApiResponse<int>> DeleteCategory(int categoryId, int deletedBy);
        Task<ApiResponse<List<Categories>>> GetAllDropDownCategories();

    }
}

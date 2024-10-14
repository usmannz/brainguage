using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service.Contracts
{
    public interface ICategoryService
    {
    Task<ApiResponse<ViewModelCategoriesListing>> GetAllCategories(Pager pagination);
    Task<ApiResponse<int>> SaveCategory(Categories category, int userId);
    Task<ApiResponse<int>> DeleteCategory(int categoryId, int deletedBy);
    }
}

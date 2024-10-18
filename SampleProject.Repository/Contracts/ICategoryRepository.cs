using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository.Contracts
{
    public interface ICategoryRepository : IRepository<Categories>
    {
    Task<ViewModelCategoriesListing> GetAllCategories(Pager pagination);
    Task<int> SaveCategory(Categories category);
    Task<int> DeleteCategory(int categoryId, int deletedBy);
    Task<List<Categories>> GetAllDropDownCategories();
    }
}

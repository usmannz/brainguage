using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository.Contracts
{
    public interface ICategoryRepository : IRepository<Categories>
    {
    Task<ViewModelCategoriesListing> GetAllCategories(Pager pagination);
    Task<int> SaveCategory(Categories category);
    Task<int> DeleteCategory(int categoryId, int deletedBy);
    Task<List<Categories>> GetAllDropDownCategories();
    Task<List<Products>> GetAllProducts();
    Task<Products> GetProductById(int productId);

    }
}

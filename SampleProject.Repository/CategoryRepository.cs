using Azure;
using Microsoft.EntityFrameworkCore;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Repository.Contracts;
using SampleProject.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository
{
    public class CategoryRepository : Repository<Categories, DBContext>, ICategoryRepository
    {
        public CategoryRepository(
            DBContext context
            // CacheHelper cacheHelper
            ) : base(context)
        {
            // _cacheHelper = cacheHelper;
        }

        public async Task<ViewModelCategoriesListing> GetAllCategories(Pager pagination)
        {
            ViewModelCategoriesListing listCategories = new ViewModelCategoriesListing();

            // Start with a queryable context for questions
            var query = _context.Categories.Where(x=> !x.IsDeleted).AsQueryable();

            if (query.Any())
            {
                // Apply filtering
                if (!string.IsNullOrEmpty(pagination.FilterText))
                {
                    query = query.Where(q => q.Name.Contains(pagination.FilterText));
                }

                // Get the total count of filtered questions
                listCategories.Count = await query.CountAsync();

                // Apply sorting based on the provided SortByField and SortDirection
                if (!string.IsNullOrEmpty(pagination.SortByField))
                {
                    if (pagination.SortDirection == (int)SortDirection.Asc)
                    {
                        query = query.OrderBy(pagination.SortByField); // Sort ascending
                    }
                    else if (pagination.SortDirection == (int)SortDirection.Desc)
                    {
                        query = query.OrderBy($"{pagination.SortByField} desc"); // Sort descending
                    }
                }

                // Apply pagination
                var pagedCategories = await query
                    .Skip(pagination.SkipBy)
                    .Take(pagination.PageSize)
                    .ToListAsync();

                // Populate the list of questions
                listCategories.Categories = pagedCategories.Select(x => new ViewCategoriesListing()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            }

            return listCategories;
        }
public async Task<int> SaveCategory(Categories category)
        {

            if (category.Id == 0)
            {

                var entityCheck = _context.Categories.AsNoTracking().FirstOrDefault(item => item.Name == category.Name && !item.IsDeleted);
                if (entityCheck != null)
                {
                    return await Task.FromResult(-1);
                }
                else
                {
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                    return category.Id;
                }
            }

            else if (category.Id != 0)

            {
                var entityCheck = _context.Categories.AsNoTracking().FirstOrDefault(item => item.Name == category.Name && item.Id != category.Id && !item.IsDeleted);
                if (entityCheck != null)
                {
                    return await Task.FromResult(-1);
                }
                else
                {
                    var entityCategoryCheck = _context.Categories.FirstOrDefault(item => item.Id == category.Id && !item.IsDeleted);
                    entityCategoryCheck.Name = category.Name;
                    entityCategoryCheck.UpdateStamp = category.UpdateStamp;
                    entityCategoryCheck.UpdatedBy = category.UpdatedBy;
                    _context.Categories.Update(entityCategoryCheck);
                    await _context.SaveChangesAsync();
                    return entityCategoryCheck.Id;
                }
            }
            else
            {
                return await Task.FromResult(-1);

            }
        }
   
     public async Task<int> DeleteCategory(int categoryId, int deletedBy)
        {  
            var question = _context.Questions.Where(x => x.CategoriesId == categoryId && !x.IsDeleted).FirstOrDefault();
              if(question != null)
              {
            return -1;
              }
            var category = _context.Categories.Where(x => x.Id == categoryId).SingleOrDefault();
            category.IsDeleted = true;
            category.DeletedBy = deletedBy;
            category.DeleteStamp = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return category.Id;
        }
 public async Task<List<Categories>> GetAllDropDownCategories()
        {
            List<Categories> listCategories = new List<Categories>();

            listCategories = await _context.Categories
                .Where(x => !x.IsDeleted)
                .AsNoTracking().ToListAsync();

            return listCategories;

        }

    }
}

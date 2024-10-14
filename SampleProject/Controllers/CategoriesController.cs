using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.API;
using SampleProject.API.Controllers;
using SampleProject.Common;
using SampleProject.Common.Entities;
using SampleProject.Common.Models;
using SampleProject.Service.Contracts;

namespace SampleProject.Controllers
{
    [Authorize, Route("api/categories")]
    public class CategoriesController : BaseController
    {
        private readonly ILogger<CategoriesController> _logger;
        private ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin"),HttpPost("GetAllCategories")] 
        public async Task <ApiResponse<ViewModelCategoriesListing>> GetAllCategories([FromBody] Pager pagination)
        {
            return await _categoryService.GetAllCategories(pagination);
        }

          [Authorize(Roles = "Admin"),HttpPost("SaveCategory")]        
        public async Task<IActionResult> SaveCategory([FromBody] Categories question)
        {
            
            return Ok(await _categoryService.SaveCategory(question,UserId));
        }
        [Authorize(Roles = "Admin"), HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            int deletedBy = this.User.GetUserId();
            return Ok(await _categoryService.DeleteCategory(categoryId, deletedBy));
        }
    }
}

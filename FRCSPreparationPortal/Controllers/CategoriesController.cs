using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FRCSPreparationPortal.API;
using FRCSPreparationPortal.API.Controllers;
using FRCSPreparationPortal.Common;
using FRCSPreparationPortal.Common.Entities;
using FRCSPreparationPortal.Common.Models;
using FRCSPreparationPortal.Service.Contracts;
using FRCSPreparationPortal.Service;

namespace FRCSPreparationPortal.Controllers
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

        [HttpGet("GetAllDropDownCategories")]
        public async Task <ApiResponse<List<Categories>>> GetAllDropDownCategories()
        {
            return await _categoryService.GetAllDropDownCategories();

        }
        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public async Task<ApiResponse<List<Products>>> GetAllProducts()
        {
            return await _categoryService.GetAllProducts();
        }

        [AllowAnonymous]
        [HttpGet("GetProductById")]
        public async Task<Products> GetProductById(int productId)
        {
            return await _categoryService.GetProductById(productId);
        }

    }
}

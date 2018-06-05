using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BloggingPlatform.Dto;
using BloggingPlatform.Api.Managers.Interfaces;

namespace BloggingPlatform.Api.Controllers
{
    [Produces("application/json")]
    [Route("category")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        
        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            return _categoryManager.GetCategories();
        }
    }
}
using AutoMapper;
using BloggingPlatform.Api.Managers.Interfaces;
using BloggingPlatform.Db.Model;
using BloggingPlatform.Dto;
using System.Collections.Generic;

namespace BloggingPlatform.Api.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly BloggingPlatformContext _context;
        private readonly IMapper _mapper;

        public CategoryManager(BloggingPlatformContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            var categories = _context.Categories;
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }
    }
}

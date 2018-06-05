using BloggingPlatform.Dto;
using System.Collections.Generic;

namespace BloggingPlatform.Api.Managers.Interfaces
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryDto> GetCategories();
    }
}

using BloggingPlatform.Db.Model;
using BloggingPlatform.Dto;
using System;
using System.Collections.Generic;

namespace BloggingPlatform.Api.Managers.Interfaces
{
    public interface IPostManager
    {
        IEnumerable<PostDto> GetPostsByAuthor(Guid author);
        IEnumerable<PostDto> GetPostsByCategory(Guid author);
        IEnumerable<PostDto> GetPosts();
        PostDto GetPostById(Guid id);
        void SavePost(PostDto post, List<PostCategoryDto> postsCategoriesDto);
    }
}

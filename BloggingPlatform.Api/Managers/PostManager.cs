using System;
using System.Collections.Generic;
using BloggingPlatform.Api.Managers.Interfaces;
using BloggingPlatform.Db.Model;
using System.Linq;
using BloggingPlatform.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Api.Managers
{
    public class PostManager : IPostManager
    {
        private readonly BloggingPlatformContext _context;
        private readonly IMapper _mapper;

        public PostManager(BloggingPlatformContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PostDto> GetPosts()
        {
            var posts = _context.Posts
                .Include(p => p.Author)
                .Include(p => p.PostsCategories).ThenInclude(pc => pc.Category)
                .ToList();

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public PostDto GetPostById(Guid id)
        {
            // var categories = new List<CategoryDto>();
            var posts = _context.Posts.Where(p => p.Id == id)
                .Include(p => p.Author)
                .Include(p => p.PostsCategories).ThenInclude(pc => pc.Category)
                .FirstOrDefault();

            var postDto = _mapper.Map<PostDto>(posts);
            // bug - assigns empty list to categories and rewrites categories got from DB
            // postDto.Categories = categories;

            return postDto;
        }

        public IEnumerable<PostDto> GetPostsByCategory(Guid categoryId)
        {
            var posts = _context.Posts.Where(p => p.PostsCategories.Any(pc => pc.CategoryId == categoryId));
            var selectedPosts = posts.Include(p => p.Author).Include(p => p.PostsCategories).ToList();

            return _mapper.Map<IEnumerable<PostDto>>(selectedPosts);
        }

        public IEnumerable<PostDto> GetPostsByAuthor(Guid authorId)
        {
            var posts = _context.Posts.Where(p => p.AuthorId == authorId)
                .Include(p => p.Author)
                .Include(p => p.PostsCategories)
                .ToList();

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public void SavePost(PostDto postDto, List<PostCategoryDto> postsCategoriesDto)
        {
            var post = _mapper.Map<Posts>(postDto);

            post.PostDate = DateTime.Now;

            var postCategories = _mapper.Map<IEnumerable<PostsCategories>>(postsCategoriesDto);

            _context.AddRange(postCategories);
            _context.Add(post);

            _context.SaveChanges();
        }
    }
}

using AutoMapper;
using BloggingPlatform.Api.Managers;
using BloggingPlatform.Db.Model;
using BloggingPlatform.Dto;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using BloggingPlatform.Test.Helpers;

namespace BloggingPlatform.Test.ManagersTests
{
    [TestFixture]
    public class PostManagerTests
    {
        private BloggingPlatformContext testDBContext;
        private DbContextOptions dbContextOptions;
        private IMapper testAutoMapper;

        private Posts TestPost1;
        private Posts TestPost2;
        private Authors TestAuthor;
        private Categories TestCategory;
        private PostsCategories TestPostsCategories;
        
        private PostDto TestPostDto1;
        private PostDto TestPostDto2;
        private AuthorDto TestAuthorDto;
        private CategoryDto TestCategoryDto;
        private PostCategoryDto TestPostsCategoriesDto;

        [SetUp]
        public void SetUp()
        {
            SetUpTestDBContext();
        }

        private void SetUpTestDBContext()
        {
            dbContextOptions = new DbContextOptionsBuilder<BloggingPlatformContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryBloggingPlatformDB")
                .Options;

            testDBContext = new BloggingPlatformContext(dbContextOptions);

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            testAutoMapper = new Mapper(autoMapperConfig);
        }

        private void AddTestPostsToDB()
        {
            TestAuthor = new Authors()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Podskalicky",
                Email = "jan.podskalicky@gmail.com",
                Password = "pass",
                Phone = "0",
                Username = "Jan"
            };

            testDBContext.Authors.Add(TestAuthor);

            TestCategory = new Categories
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            testDBContext.Categories.Add(TestCategory);

            TestPost1 = new Posts()
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Perex = "This is a test post 1",
                PostText = "This is a text of test post 1",
                PostDate = DateTime.Now,
                AuthorId = TestAuthor.Id
            };

            TestPost2 = new Posts()
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 2",
                Perex = "This is a test post 2",
                PostText = "This is a text of test post 2",
                PostDate = DateTime.Now,
                AuthorId = TestAuthor.Id
            };
            
            testDBContext.Posts.Add(TestPost1);
            testDBContext.Posts.Add(TestPost2);

            TestPostsCategories = new PostsCategories
            {
                Id = Guid.NewGuid(),
                CategoryId = TestCategory.Id,
                PostId = TestPost1.Id
            };

            testDBContext.PostsCategories.Add(TestPostsCategories);

            testDBContext.SaveChanges();

            TestPostDto1 = testAutoMapper.Map<PostDto>(TestPost1);
            TestPostDto2 = testAutoMapper.Map<PostDto>(TestPost2);
            TestAuthorDto = testAutoMapper.Map<AuthorDto>(TestAuthor);
            TestPostsCategoriesDto = testAutoMapper.Map<PostCategoryDto>(TestPostsCategories);
            TestCategoryDto = testAutoMapper.Map<CategoryDto>(TestCategory);
        }

        [TearDown]
        public void TearDown()
        {
            testDBContext.Database.EnsureDeleted();
            testDBContext.Dispose();
        }

        [Test]
        public void GetPosts_ReturnAllPosts()
        {
            AddTestPostsToDB();

            var testPostManager = new PostManager(testDBContext, testAutoMapper);

            var posts = testPostManager.GetPosts();

            Assert.AreEqual(2, posts.ToList().Count);
        }

        [Test]
        public void GetPostById_ReturnCorrectPost()
        {
            AddTestPostsToDB();

            var testPostManager = new PostManager(testDBContext, testAutoMapper);

            var post = testPostManager.GetPostById(TestPost1.Id);

            TestPostDto1.AssertAreEqual(post);
        }

        [Test]
        public void GetPostByCategory_ReturnCorrectPost()
        {
            AddTestPostsToDB();

            var testPostManager = new PostManager(testDBContext, testAutoMapper);

            var posts = testPostManager.GetPostsByCategory(TestCategory.Id).ToList();

            Assert.AreEqual(1, posts.Count);
            TestPostDto1.AssertContains(posts);
        }

        [Test]
        public void GetPostByAuthor_ReturnCorrectPost()
        {
            AddTestPostsToDB();

            var testPostManager = new PostManager(testDBContext, testAutoMapper);

            var posts = testPostManager.GetPostsByAuthor(TestAuthor.Id).ToList();

            Assert.AreEqual(2, posts.Count);
            TestPostDto1.AssertContains(posts);
            TestPostDto2.AssertContains(posts);
        }

        [Test]
        public void SavePost_SavesPost()
        {
            TestAuthor = new Authors()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Podskalicky",
                Email = "jan.podskalicky@gmail.com",
                Password = "pass",
                Phone = "0",
                Username = "Jan"
            };

            testDBContext.Authors.Add(TestAuthor);

            TestCategory = new Categories
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            testDBContext.Categories.Add(TestCategory);

            testDBContext.SaveChanges();

            var testPostDto = new PostDto()
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Perex = "This is a test post 1",
                Content = "This is a text of test post 1",
                Author = testAutoMapper.Map<AuthorDto>(TestAuthor),
                Categories = new List<CategoryDto>() { testAutoMapper.Map<CategoryDto>(TestCategory), }
            };

            TestPostsCategories = new PostsCategories
            {
                Id = Guid.NewGuid(),
                CategoryId = TestCategory.Id,
                PostId = testPostDto.Id
            };

            TestPostsCategoriesDto = testAutoMapper.Map<PostCategoryDto>(TestPostsCategories);

            var testPostManager = new PostManager(testDBContext, testAutoMapper);

            testPostManager.SavePost(testPostDto, new List<PostCategoryDto> { TestPostsCategoriesDto });

            var retrievedPost = testPostManager.GetPostById(testPostDto.Id);

            testPostDto.AssertAreEqual(retrievedPost);
        }
    }
}

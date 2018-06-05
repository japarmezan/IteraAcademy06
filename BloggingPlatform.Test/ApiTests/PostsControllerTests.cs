using BloggingPlatform.Api.Controllers;
using NUnit.Framework;
using Moq;
using System;
using BloggingPlatform.Api.Managers.Interfaces;
using System.Collections.Generic;
using BloggingPlatform.Dto;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BloggingPlatform.Test.Helpers;

namespace BloggingPlatform.Test.ApiTests
{
    [TestFixture]
    public class PostsControllerTests
    {
        private PostDto TestPost1;
        private PostDto TestPost2;

        private AuthorDto TestAuthor;
        private CategoryDto TestCategory;

        [SetUp]
        public void SetupTestPosts()
        {
            TestCategory = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            };

            TestAuthor = new AuthorDto()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jan",
                LastName = "Podskalicky",
                Email = "jan.podskalicky@gmail.com"
            };

            TestPost1 = new PostDto()
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 1",
                Perex = "This is a test post 1",
                Content = "This is a text of test post 1",
                Categories = new List<CategoryDto>(),
                Author = TestAuthor
            };

            TestPost2 = new PostDto()
            {
                Id = Guid.NewGuid(),
                Title = "Test Post 2",
                Perex = "This is a test post 2",
                Content = "This is a text of test post 2",
                Categories = new List<CategoryDto>(),
                Author = TestAuthor
            };
        }

        [Test]
        public void GetAllPosts_ReturnAllPosts()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            mockManager.Setup(manager => manager.GetPosts()).Returns(GetTestPosts());

            var testPostController = new PostsController(mockManager.Object);

            // Act
            var posts = testPostController.GetPosts();

            //Assert
            Assert.IsNotNull(posts);
            Assert.AreEqual(2, posts.ToList().Count);
        }

        [Test]
        public void GetPostById_ReturnCorrectPost()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            mockManager.Setup(x => x.GetPostById(It.IsAny<Guid>()))
                        .Returns<Guid>(id => GetTestPostById(id));

            var testPostController = new PostsController(mockManager.Object);

            // Act
            var post = testPostController.GetPostById(TestPost1.Id);

            //Assert
            TestPost1.AssertAreEqual(post);
        }

        [Test]
        public void GetPostsByCategory_ReturnCorrectPosts()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            mockManager.Setup(x => x.GetPostsByCategory(It.IsAny<Guid>()))
                        .Returns<Guid>(id => GetTestPostsByCategory(id));

            var testPostController = new PostsController(mockManager.Object);

            // Act
            var posts = testPostController.GetPostsByCategory(TestCategory.Id).ToList();

            //Assert
            Assert.AreEqual(1, posts.Count);
            TestPost1.AssertContains(posts);
        }

        [Test]
        public void GetPostsByAuthor_ReturnCorrectPosts()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            mockManager.Setup(x => x.GetPostsByAuthor(It.IsAny<Guid>()))
                        .Returns<Guid>(id => GetTestPostsByAuthor(id));

            var testPostController = new PostsController(mockManager.Object);

            // Act
            var posts = testPostController.GetPostsByAuthor(TestAuthor.Id).ToList();

            //Assert
            Assert.AreEqual(2, posts.Count);
            TestPost1.AssertContains(posts);
            TestPost2.AssertContains(posts);
        }

        [Test]
        public void PostPost_ReturnSamePost_WithEmptyCategories()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            var testPostController = new PostsController(mockManager.Object);

            // Act
            var actionResult = testPostController.PostPosts(TestPost1);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var post = (actionResult as OkObjectResult).Value as PostDto;
            Assert.IsNotNull(post);

            TestPost1.AssertAreEqual(post);
        }

        [Test]
        public void PostPost_ReturnSamePost_WithCategories()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            var testPostController = new PostsController(mockManager.Object);

            TestPost1.Categories.Add(new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Test category"
            });

            // Act
            var actionResult = testPostController.PostPosts(TestPost1);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            var post = (actionResult as OkObjectResult).Value as PostDto;
            Assert.IsNotNull(post);

            TestPost1.AssertAreEqual(post);
        }

        [Test]
        public void PostPost_ReturnNotFound_WhenPostCategoryDoesNotExist()
        {
            // Arrange
            var mockManager = new Mock<IPostManager>();
            var testPostController = new PostsController(mockManager.Object);

            var testPost = TestPost1;
            testPost.Categories = new List<CategoryDto>
            {
                new CategoryDto
                {
                    Name = "This category does not exist"
                }
            };

            // Act
            var actionResult = testPostController.PostPosts(testPost);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult);
        }
        
        private List<PostDto> GetTestPosts()
        {
            var testPosts = new List<PostDto>();

            testPosts.Add(TestPost1);
            testPosts.Add(TestPost2);

            return testPosts;
        }

        private PostDto GetTestPostById(Guid id)
        {
            var testPosts = new List<PostDto>
            {
                TestPost1, TestPost2
            };

            return testPosts.FirstOrDefault(x => x.Id.Equals(id));
        }

        private IEnumerable<PostDto> GetTestPostsByCategory(Guid categoryId)
        {
            TestPost1.Categories.Add(TestCategory);

            var testPosts = new List<PostDto>
            {
                TestPost1, TestPost2
            };

            return testPosts.Where(x => x.Categories.Any(c => c.Id.Equals(categoryId)));
        }

        private IEnumerable<PostDto> GetTestPostsByAuthor(Guid authorId)
        {
            var testPosts = new List<PostDto>
            {
                TestPost1, TestPost2
            };

            return testPosts.Where(x => x.Author.Id.Equals(authorId));
        }
    }
}

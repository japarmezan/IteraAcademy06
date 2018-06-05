using BloggingPlatform.Dto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Test.Helpers
{
    public static class DtoComparers
    {
        public static void AssertAreEqual(this PostDto expected, PostDto actual)
        {
            Assert.NotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            expected.Author.AssertAreEqual(actual.Author);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Perex, actual.Perex);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.Categories.Count, actual.Categories.Count);

            foreach (var category in expected.Categories)
            {
                category.AssertContains(actual.Categories);
            }
        }

        public static void AssertAreEqual(this CategoryDto expected, CategoryDto actual)
        {
            Assert.NotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        public static void AssertAreEqual(this AuthorDto expected, AuthorDto actual)
        {
            Assert.NotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }

        public static void AssertAreEqual(this CommentDto expected, CommentDto actual)
        {
            Assert.NotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.AuthorId, actual.AuthorId);
            Assert.AreEqual(expected.CommentDate, actual.CommentDate);
            Assert.AreEqual(expected.PostId, actual.PostId);
        }

        public static void AssertContains(this PostDto expected, List<PostDto> postsList)
        {
            var actualPost = postsList.Find(p => p.Id.Equals(expected.Id));

            Assert.That(actualPost != null, $"Collection of posts does not contain post {expected}");
            expected.Author.AssertAreEqual(actualPost.Author);
            Assert.AreEqual(expected.Title, actualPost.Title, $"Collection of posts does not contain post with: expected {expected.Title}, actual {actualPost.Title}.");
            Assert.AreEqual(expected.Perex, actualPost.Perex, $"Collection of posts does not contain post with: expected {expected.Perex}, actual {actualPost.Perex}.");
            Assert.AreEqual(expected.Content, actualPost.Content, $"Collection of posts does not contain post with: expected {expected.Content}, actual {actualPost.Content}.");
            Assert.AreEqual(expected.Categories.Count, actualPost.Categories.Count, $"Collection of posts does not contain post with: expected {expected.Categories.Count}, actual {actualPost.Categories.Count}.");

            foreach (var category in expected.Categories)
            {
                var actualCategory = actualPost.Categories.Find(c => c.Id.Equals(category.Id));

                category.AssertAreEqual(actualCategory);
            }
        }

        public static void AssertContains(this CategoryDto expected, List<CategoryDto> postsList)
        {
            var actualCategory = postsList.Find(c => c.Id.Equals(expected.Id));

            Assert.That(actualCategory != null, $"Collection of posts does not contain post {expected}");
            Assert.AreEqual(expected.Name, actualCategory.Name, $"Collection of posts does not contain post with: expected {expected.Name}, actual {actualCategory.Name}.");
        }
    }
}

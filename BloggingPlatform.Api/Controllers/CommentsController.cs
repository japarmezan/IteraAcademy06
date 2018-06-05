using Microsoft.AspNetCore.Mvc;
using BloggingPlatform.Api.Managers.Interfaces;
using BloggingPlatform.Dto;
using System;
using System.Collections.Generic;

namespace BloggingPlatform.Api.Controllers
{
    [Produces("application/json")]
    [Route("comment")]
    public class CommentsController : Controller
    {
        private readonly ICommentManager _commentManager;
        private readonly IAuthorManager _authorManager;

        public CommentsController(ICommentManager commentManager, IAuthorManager authorManager)
        {
            _commentManager = commentManager;
            _authorManager = authorManager;
        }

        [HttpGet("post/{id}")]
        public IEnumerable<CommentDto> GetComments(Guid id)
        {
            return _commentManager.GetComments(id);
        }

        [HttpPost("post/{id}")]
        public IActionResult CreateComment([FromBody] CommentDto comment)
        {
            var authorId = _authorManager.GetAutor(comment.Author);
            if (authorId == null)
            {
                return NotFound("author neexistuje ");
            }
            else
            {
                comment.AuthorId = authorId.Id;
            }

            _commentManager.PostComment(comment);
            return Ok(comment);
        }
    }
}
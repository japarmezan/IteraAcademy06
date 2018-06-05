using AutoMapper;
using BloggingPlatform.Api.Managers.Interfaces;
using BloggingPlatform.Db.Model;
using BloggingPlatform.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloggingPlatform.Api.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly BloggingPlatformContext _context;
        private readonly IMapper _mapper;

        public CommentManager(BloggingPlatformContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CommentDto> GetComments(Guid id)
        {
            var comments = _context.Comments.Where(c => c.PostId.Equals(id)).Include(c => c.Author).Include(c => c.Post);
            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public void PostComment(CommentDto commentDto)
        {
            commentDto.Id = Guid.NewGuid();
            commentDto.CommentDate = DateTime.Now;

            var comment = _mapper.Map<Comments>(commentDto);
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}

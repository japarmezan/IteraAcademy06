using BloggingPlatform.Dto;
using System;
using System.Collections.Generic;

namespace BloggingPlatform.Api.Managers.Interfaces
{
    public interface ICommentManager
    {
        IEnumerable<CommentDto> GetComments(Guid id);
        void PostComment(CommentDto comment);
    }
}

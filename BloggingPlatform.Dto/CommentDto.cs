using System;

namespace BloggingPlatform.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public DateTime CommentDate { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; }
        public Guid PostId { get; set; }

        public string Author { get; set; }
        public PostDto Post { get; set; }
    }
}

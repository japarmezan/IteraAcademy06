using System;

namespace BloggingPlatform.Dto
{
    public class PostCategoryDto
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

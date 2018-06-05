using System;
using System.Collections.Generic;

namespace BloggingPlatform.Dto
{
    public class PostDto
    {
        public Guid Id;
        public AuthorDto Author { get; set; }
        public string Title { get; set; }
        public string Perex { get; set; }
        //public string Comments { get; set; }
        public string Content { get; set; }
        //public DateTime PostDate { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatform.Db.Model
{
    public partial class PostsCategories
    {
        public Guid Id { get; set; }
        [Column("postId")]
        public Guid PostId { get; set; }
        [Column("categoryId")]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("PostsCategories")]
        public Categories Category { get; set; }
        [ForeignKey("PostId")]
        [InverseProperty("PostsCategories")]
        public Posts Post { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatform.Db.Model
{
    public partial class Comments
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("commentDate", TypeName = "datetime")]
        public DateTime CommentDate { get; set; }
        [Column("authorId")]
        public Guid AuthorId { get; set; }
        [Required]
        [Column("commentText")]
        public string CommentText { get; set; }
        [Column("postId")]
        public Guid PostId { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Comments")]
        public Authors Author { get; set; }
        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public Posts Post { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatform.Db.Model
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
            PostsCategories = new HashSet<PostsCategories>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(250)]
        public string Title { get; set; }
        [Column("postDate", TypeName = "datetime")]
        public DateTime PostDate { get; set; }
        [Column("authorId")]
        public Guid AuthorId { get; set; }
        [Column("perex")]
        public string Perex { get; set; }
        [Required]
        [Column("postText")]
        public string PostText { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Posts")]
        public Authors Author { get; set; }
        [InverseProperty("Post")]
        public ICollection<Comments> Comments { get; set; }
        [InverseProperty("Post")]
        public ICollection<PostsCategories> PostsCategories { get; set; }
    }
}

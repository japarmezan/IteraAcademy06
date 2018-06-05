using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatform.Db.Model
{
    public partial class Categories
    {
        public Categories()
        {
            PostsCategories = new HashSet<PostsCategories>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(250)]
        public string Name { get; set; }

        [InverseProperty("Category")]
        public ICollection<PostsCategories> PostsCategories { get; set; }
    }
}

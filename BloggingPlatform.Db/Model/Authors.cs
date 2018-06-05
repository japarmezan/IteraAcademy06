using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingPlatform.Db.Model
{
    public partial class Authors
    {
        public Authors()
        {
            Comments = new HashSet<Comments>();
            Posts = new HashSet<Posts>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("username")]
        [StringLength(250)]
        public string Username { get; set; }
        [Column("firstName")]
        [StringLength(250)]
        public string FirstName { get; set; }
        [Column("lastName")]
        [StringLength(250)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(250)]
        public string Password { get; set; }
        [Column("phone")]
        [StringLength(50)]
        public string Phone { get; set; }

        [InverseProperty("Author")]
        public ICollection<Comments> Comments { get; set; }
        [InverseProperty("Author")]
        public ICollection<Posts> Posts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCbook2.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10000,MinimumLength =2)]
        public string Content { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int Likes { get; set; }

        public string UserId { get; set; }

        [StringLength(100)]
        public string PostedBy { get; set; }

        [ForeignKey("PostId")]
        public ICollection<Comment> Comments { get; set; }
    }
}

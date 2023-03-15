using System.ComponentModel.DataAnnotations;
using System;

namespace CCbook2.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Content { get; set; }

        [Required]
        public string CommentorName { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int PostId { get; set; }


        
    }
}

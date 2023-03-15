using CCbook2.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CCbook2.Data
{

    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(100)]
        public  string Name { get; set; }

        public int Followers { get; set; }

        public int Following {get; set; }

        [ForeignKey("UserId")]
        public ICollection<Post> Posts { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Post { get;set; }

        public DbSet<Comment> Comment { get; set; }
    }
}

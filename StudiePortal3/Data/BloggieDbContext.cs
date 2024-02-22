using Microsoft.EntityFrameworkCore;
using StudiePortal3.Models.Entities;
using System.Collections.Generic;

namespace StudiePortal3.Data
{
    public class BloggieDbContext : DbContext
    {


        public BloggieDbContext(DbContextOptions<BloggieDbContext> options) : base(options)
        {
        }



        public DbSet<Student> Students { get; set; }
    }
}

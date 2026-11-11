using Microsoft.EntityFrameworkCore;
using SocialHub.Models;

namespace SocialHub.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
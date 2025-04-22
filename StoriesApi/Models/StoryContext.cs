using Microsoft.EntityFrameworkCore;

namespace StoriesApi.Models;

public class StoryContext : DbContext
{
    public StoryContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Story> Stories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}
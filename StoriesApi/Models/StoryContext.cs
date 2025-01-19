using Microsoft.EntityFrameworkCore;

namespace StoriesApi.Models;

public class StoryContext : DbContext
{
    public StoryContext(DbContextOptions<StoryContext> options)
        : base(options)
    {
    }

    public DbSet<Story> Stories { get; set; } = null!;
}
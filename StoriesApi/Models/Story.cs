namespace StoriesApi.Models;

public class Story
{
    public int StoryId { get; private set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int PostCount { get; set; }
    public DateTime? LastPost { get; set; }
}
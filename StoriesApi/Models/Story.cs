namespace StoriesApi.Models;

public class Story
{
    public required int StoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int PostCount { get; set; }
    public DateTime LastPost { get; set; }
}
namespace StoriesApi.Models;

public class Post
{
    public required int PostId { get; set; }
    public required string Content { get; set; }
    public required string Author { get; set; }
    public required DateTime Date { get; set; }
    public required int StoryId { get; set; }
}
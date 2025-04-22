namespace StoriesApi.Models;

public class User
{
    public int UserId { get; private set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
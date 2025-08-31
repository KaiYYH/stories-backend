using StoriesApi.Models;

namespace StoriesApi.Models.Responses;

public class MultigetUserResponse
{
    public required Dictionary<int, User> Users { get; set; }

}
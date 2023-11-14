using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BlogPost.Models;

public class Post
{
    [JsonPropertyName("UserId")]
    public int UserId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Description { get; set; } = string.Empty;
}

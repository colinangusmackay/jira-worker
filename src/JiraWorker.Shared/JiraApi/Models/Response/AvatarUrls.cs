using System.Text.Json.Serialization;

namespace JiraWorker.JiraApi.Models.Response;

public class AvatarUrls
{
    [JsonPropertyName("16x16")] 
    public string SixteenPixels { get; init; }
    
    [JsonPropertyName("24x24")] 
    public string TwentyFourPixels { get; init; }
    
    [JsonPropertyName("32x32")] 
    public string ThirtyTwoPixels { get; init; }

    [JsonPropertyName("48x48")] 
    public string FortyEightPixels { get; init; }
}
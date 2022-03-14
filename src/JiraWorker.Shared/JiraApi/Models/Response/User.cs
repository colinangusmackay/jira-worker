using System.Text.Json.Serialization;

namespace JiraWorker.JiraApi.Models.Response;

public class User : JiraBaseEntity
{
    public static readonly User NullUser = new ()
    {
        AccountType = "null",
        EmailAddress = "null@example.com",
        DisplayName = "**NULL**",
    };

    public string AccountId { get; init; }
    public string AccountType { get; init; }
    public string? EmailAddress { get; init; }
    public string DisplayName { get; init; }
    public bool Active { get; init; }
    public string TimeZone { get; init; }
    public string Locale { get; init; }
}

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
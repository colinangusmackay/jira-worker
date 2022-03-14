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
    
    public AvatarUrls AvatarUrls { get; init; }
}
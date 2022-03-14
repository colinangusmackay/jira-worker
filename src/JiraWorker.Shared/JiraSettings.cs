namespace JiraWorker.JiraApi;

public class JiraSettings
{
    public class BasicAuthSettings
    {
        public string? EmailAddress { get; set; }
        public string? Token { get; set; }
    }

    public JiraSettings()
    {
        BasicAuth = new ();
    }
    
    public string? BaseUrl { get; set; }
    public BasicAuthSettings BasicAuth { get; set; }

    public string CurrentUserEmailAddress() => BasicAuth.EmailAddress ?? "unknown@example.com";
}
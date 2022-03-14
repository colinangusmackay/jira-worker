using Refit;

namespace JiraWorker.JiraApi.Models.Request;

public class UserSearch : PagedRequest
{
    [AliasAs("query")]
    public string? Query { get; set; }
    
    [AliasAs("username")]
    public string? UserName { get; set; }
    
    [AliasAs("accountId")]
    public string? AccountId { get; set; }
}
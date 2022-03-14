using Refit;

namespace JiraWorker.JiraApi.Models.Request;

public class PagedRequest
{
    [AliasAs("startAt")]
    public int StartAt { get; set; } = 0;
    
    [AliasAs("maxResults")]
    public int MaxResults { get; set; } = 50;
}
using JiraWorker.JiraApi.Models.Request;
using JiraWorker.JiraApi.Models.Response;
using Refit;

namespace JiraWorker.JiraApi;

public interface IUsers
{
    [Get("/2/user/search")]
    Task<User[]> SearchAsync(UserSearch request);
}
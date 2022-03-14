using System.Collections.Concurrent;
using JiraWorker.JiraApi;
using JiraWorker.JiraApi.Models.Request;
using JiraWorker.JiraApi.Models.Response;

namespace JiraWorker.Services;

public interface IUserService
{
    Task<User> GetUserAsync(string emailAddress);
}

public class UserService : IUserService
{
    private readonly IUsers _userApi;
    private ConcurrentDictionary<string, User> _users;

    public UserService(IUsers userApi)
    {
        _userApi = userApi;
        _users = new ConcurrentDictionary<string, User>(StringComparer.OrdinalIgnoreCase);
    }
    
    public async Task<User> GetUserAsync(string emailAddress)
    {
        if (_users.TryGetValue(emailAddress, out var user))
            return user;

        var users = await _userApi.SearchAsync(new UserSearch {Query = emailAddress});
        user = users.FirstOrDefault(u =>
            (u.EmailAddress ?? string.Empty).Equals(emailAddress, StringComparison.OrdinalIgnoreCase), User.NullUser);
        _users.TryAdd(emailAddress, user);

        return user;
    }
}
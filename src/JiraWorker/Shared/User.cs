using JiraWorker.JiraApi;
using JiraWorker.Services;
using Microsoft.Extensions.Options;

namespace JiraWorker.Shared;

public partial class User
{
    public string UserName { get; private set; } = "Unknown";
    
    protected override async Task OnInitializedAsync()
    {
        var user = await _userService.GetUserAsync(_settings.Value.CurrentUserEmailAddress());
        UserName = user.DisplayName;
        await base.OnInitializedAsync();
    }
}
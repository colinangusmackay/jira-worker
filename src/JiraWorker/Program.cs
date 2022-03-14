using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using JiraWorker;
using JiraWorker.JiraApi;
using JiraWorker.JiraApi.Models.Request;
using JiraWorker.JiraApi.Models.Response;
using JiraWorker.Services;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var jiraSettings = builder.Configuration.GetSection("JiraSettings");
builder.Services.Configure<JiraSettings>(s => jiraSettings.Bind(s));
RefitSettings refitSettings = new RefitSettings();
builder.Services
    .AddRefitClient<IUsers>(refitSettings)
    .ConfigureHttpClient((p, c) =>
    {
        var settings = p.GetRequiredService<IOptions<JiraSettings>>();
        var baseUrl = settings.Value.BaseUrl ??
                      throw new InvalidOperationException("The base URL is not set up in the app settings.");
        c.BaseAddress = new Uri(baseUrl);
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

builder.Services.AddSingleton<IUserService, UserService>();

await builder.Build().RunAsync();
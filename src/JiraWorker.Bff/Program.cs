using System.Net.Http.Headers;
using JiraWorker.Bff;
using JiraWorker.JiraApi;
using JiraWorker.JiraApi.Models.Response;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jiraSettings = builder.Configuration.GetSection("JiraSettings");
builder.Services.Configure<JiraSettings>(s => jiraSettings.Bind(s));
builder.Services.AddSingleton<ForwardToJiraMiddleware>();
builder.Services.AddHttpClient(nameof(ForwardToJiraMiddleware), (p, c) =>
{
    var settings = p.GetRequiredService<IOptions<JiraSettings>>();
    var baseUrl = settings.Value.BaseUrl ??
                  throw new InvalidOperationException("The base URL is not set up in the app settings.");
    c.BaseAddress = new Uri(baseUrl);
    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
})
.AddHttpMessageHandler<JiraBasicAuthHandler>();
builder.Services.AddTransient<JiraBasicAuthHandler>();

var app = builder.Build();

app.UseCors(cors =>
{
    cors
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseMiddleware<ForwardToJiraMiddleware>();

app.Run();
using System.Text;
using Microsoft.Extensions.Primitives;

namespace JiraWorker.Bff;

public class ForwardToJiraMiddleware
{
    public ForwardToJiraMiddleware(RequestDelegate next)
    {
    }
    
    public async Task InvokeAsync(HttpContext context, IHttpClientFactory forwardingClientFactory)
    {
        var forwardingClient = forwardingClientFactory.CreateClient(nameof(ForwardToJiraMiddleware));
        var path = context.Request.Path;
        var query = context.Request.QueryString;

        HttpResponseMessage response;
        if (context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            response = await ForwardGetAsync(forwardingClient, path, query);
        else
            response = await ForwardAsync(forwardingClient, context.Request);

        await ProcessResponseAsync(context, response);
    }

    private async Task ProcessResponseAsync(HttpContext context, HttpResponseMessage response)
    {
        foreach (var header in response.Headers)
        {
            var key = $"x-fwd-{header.Key}";
            var values = new StringValues(header.Value.ToArray());
            context.Response.Headers.Add(key, values);
        }

        if (response.Content.Headers.ContentType != null)
            context.Response.ContentType = response.Content.Headers.ContentType.ToString();
        await response.Content.CopyToAsync(context.Response.Body);
    }

    private async Task<HttpResponseMessage> ForwardAsync(HttpClient forwardingClient, HttpRequest request)
    {
        var path = request.Path;
        var query = request.QueryString;
        Uri uri = new Uri(path + query);

        var message = new HttpRequestMessage();
        message.Method = new HttpMethod(request.Method);
        message.RequestUri = uri;
        using var reader = new StreamReader(request.Body);
        var contentBody = await reader.ReadToEndAsync();

        using var content = new StringContent(contentBody, Encoding.UTF8, "application/json");
        message.Content = content;
        return await forwardingClient.SendAsync(message);
    }

    private async Task<HttpResponseMessage> ForwardGetAsync(HttpClient forwardingClient, PathString path, QueryString query)
    {
        string requestUri = path + query;
        var response = await forwardingClient.GetAsync(requestUri);
        return response;
    }
}
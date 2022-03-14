using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;

namespace JiraWorker.JiraApi;

public class JiraBasicAuthHandler : DelegatingHandler
{
    private const string Scheme = "Basic";
    private readonly AuthenticationHeaderValue _header;

    public JiraBasicAuthHandler(IOptions<JiraSettings> settings)
    {
        var headerParameters = $"{settings.Value.BasicAuth.EmailAddress}:{settings.Value.BasicAuth.Token}";
        var rawBytes = Encoding.ASCII.GetBytes(headerParameters);
        var b64HeaderParameter = Convert.ToBase64String(rawBytes);
        _header = new AuthenticationHeaderValue(Scheme, b64HeaderParameter);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = _header;
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
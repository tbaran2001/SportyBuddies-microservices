using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks;

public class AuthHeaderHandler(IHttpContextAccessor httpContext) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = (httpContext?.HttpContext?.Request.Headers["Authorization"])?.ToString();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token?.Replace("Bearer ", "", StringComparison.CurrentCulture));

        return base.SendAsync(request, cancellationToken);
    }
}
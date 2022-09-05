using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace LibHouse.API.Extensions.Http
{
    internal static class HttpRequestExtensions
    {
        public static string GetBearerTokenValueFromAuthorizationHeader(this HttpRequest httpRequest) =>
            httpRequest.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
    }
}
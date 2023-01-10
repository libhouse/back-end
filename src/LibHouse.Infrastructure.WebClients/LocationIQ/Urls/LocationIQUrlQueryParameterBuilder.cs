using LibHouse.Infrastructure.WebClients.LocationIQ.Parameters;
using System.Text;

namespace LibHouse.Infrastructure.WebClients.LocationIQ.Urls
{
    internal static class LocationIQUrlQueryParameterBuilder
    {
        internal static string BuildQueryParametersForSearchForwardGeocoding(
            ForwardGeocodingStructuredRequestParameter parameters,
            string accessToken,
            string format)
        {
            StringBuilder queryParameters = new($"key={accessToken}");
            queryParameters.Append($"&format={format}");
            queryParameters.Append($"&country={parameters.Country}");
            queryParameters.Append($"&street={parameters.Street}");
            queryParameters.Append($"&city={parameters.City}");
            queryParameters.Append($"&county={parameters.County}");
            queryParameters.Append($"&state={parameters.State}");
            queryParameters.Append($"&limit={parameters.Limit}");
            return queryParameters.ToString();
        }
    }
}
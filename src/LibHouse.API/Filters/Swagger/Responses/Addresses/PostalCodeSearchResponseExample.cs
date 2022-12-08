using LibHouse.Infrastructure.Controllers.Responses.Localizations;
using Swashbuckle.AspNetCore.Filters;

namespace LibHouse.API.Filters.Swagger.Responses.Addresses
{
    public class PostalCodeSearchResponseExample : IExamplesProvider<PostalCodeSearchResponse>
    {
        public PostalCodeSearchResponse GetExamples()
        {
            return new()
            {
                Complement = "de 321 ao fim - lado ímpar",
                FederativeUnit = "SP",
                Localization = "São Paulo",
                Neighborhood = "Centro",
                Street = "Rua São Bento"
            };
        }
    }
}
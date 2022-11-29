using LibHouse.Business.Application.Localizations.Outputs;
using LibHouse.Business.Monads;
using LibHouse.Infrastructure.Controllers.Http.Localizations.Converters;
using LibHouse.Infrastructure.Controllers.Http.Localizations.Interfaces;
using LibHouse.Infrastructure.Controllers.Responses.Localizations;
using LibHouse.Infrastructure.Controllers.ViewModels.Localizations;
using System;
using System.Threading.Tasks;

namespace LibHouse.Infrastructure.Controllers.Http.Localizations.Adapters
{
    public class AddressWebApiAdapter : IHttpAddress
    {
        private Func<string, Task<OutputPostalCodeSearch>> OnPostalCodeSearchFunction { get; set; }

        public void OnPostalCodeSearch(Func<string, Task<OutputPostalCodeSearch>> on) => OnPostalCodeSearchFunction = on;

        public async Task<Result<PostalCodeSearchResponse>> PostalCodeSearch(PostalCodeSearchViewModel postalCodeSearchViewModel)
        {
            OutputPostalCodeSearch output = await OnPostalCodeSearchFunction(postalCodeSearchViewModel.PostalCodeNumber);
            return output.IsSuccess ? Result.Success(output.Convert()) : Result.Fail<PostalCodeSearchResponse>(output.PostalCodeSearchMessage);
        }
    }
}
﻿using LibHouse.Business.Application.Localizations.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Localizations.Interfaces;

namespace LibHouse.Infrastructure.Controllers.Http.Localizations
{
    public class AddressController
    {
        public AddressController(
            IHttpAddress addressWebApiAdapter, 
            IPostalCodeSearch postalCodeSearch)
        {
            addressWebApiAdapter.OnPostalCodeSearch(
                async (input) =>
                {
                    return await postalCodeSearch.ExecuteAsync(input);
                }
            );
        }
    }
}
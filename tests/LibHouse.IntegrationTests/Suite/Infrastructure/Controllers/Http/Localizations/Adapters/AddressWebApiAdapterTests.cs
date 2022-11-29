using LibHouse.Business.Application.Localizations;
using LibHouse.Business.Application.Localizations.Gateways;
using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Localizations;
using LibHouse.Infrastructure.Cache.Configurations;
using LibHouse.Infrastructure.Cache.Decorators.Memory;
using LibHouse.Infrastructure.Cache.Providers;
using LibHouse.Infrastructure.Controllers.Http.Localizations;
using LibHouse.Infrastructure.Controllers.Http.Localizations.Adapters;
using LibHouse.Infrastructure.Controllers.Responses.Localizations;
using LibHouse.Infrastructure.Controllers.ViewModels.Localizations;
using LibHouse.Infrastructure.WebClients.ViaCep;
using LibHouse.Infrastructure.WebClients.ViaCep.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Infrastructure.Controllers.Http.Localizations.Adapters
{
    [Collection("Infrastructure.Controllers")]
    public class AddressWebApiAdapterTests
    {
        private readonly IConfiguration _testsConfiguration;

        public AddressWebApiAdapterTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task PostalCodeSearch_ValidPostalCodeNumber_ShouldBeSuccess()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            IAddressRepository addressRepository = new AddressRepository(libHouseContext);
            MemoryCaching memoryCaching = new(Options.Create(new MemoryCachingConfiguration
            {
                CacheEntrySize = 1,
                CacheSizeLimit = 200
            }));
            IAddressRepository addressDecorator = new AddressMemoryCachingDecorator<IAddressRepository>(addressRepository, memoryCaching);
            ViaCepWebClient viaCepWebClient = new(new HttpClient(), new()
            {
                BaseUrl = "https://viacep.com.br/ws",
                EndpointResponseType = "json"
            });
            IAddressPostalCodeGateway addressPostalCodeGateway = new ViaCepAddressGateway(viaCepWebClient);
            PostalCodeSearch postalCodeSearch = new(notifier, addressDecorator, addressPostalCodeGateway);
            AddressWebApiAdapter addressWebApiAdapter = new();
            _ = new AddressController(addressWebApiAdapter, postalCodeSearch);
            PostalCodeSearchViewModel viewModel = new() { PostalCodeNumber = "01001000" };
            Result<PostalCodeSearchResponse> postalCodeSearchResult = await addressWebApiAdapter.PostalCodeSearch(viewModel);
            Assert.True(postalCodeSearchResult.IsSuccess);
        }
    }
}
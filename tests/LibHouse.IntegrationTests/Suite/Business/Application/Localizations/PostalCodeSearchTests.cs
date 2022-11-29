using LibHouse.Business.Application.Localizations;
using LibHouse.Business.Application.Localizations.Gateways;
using LibHouse.Business.Application.Localizations.Outputs;
using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Notifiers;
using LibHouse.Data.Context;
using LibHouse.Data.Extensions.Context;
using LibHouse.Data.Repositories.Localizations;
using LibHouse.Infrastructure.Cache.Configurations;
using LibHouse.Infrastructure.Cache.Decorators.Memory;
using LibHouse.Infrastructure.Cache.Providers;
using LibHouse.Infrastructure.WebClients.ViaCep;
using LibHouse.Infrastructure.WebClients.ViaCep.Gateways;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LibHouse.IntegrationTests.Suite.Business.Application.Localizations
{
    [Collection("Business.Application")]
    public class PostalCodeSearchTests
    {
        private readonly IConfiguration _testsConfiguration;

        public PostalCodeSearchTests()
        {
            _testsConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.Tests.json").Build();
        }

        [Fact]
        public async Task ExecuteAsync_ValidPostalCodeNumberNotRegisteredInDatabase_ShouldGetPostalCodeAddress()
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
            string postalCodeNumber = "01001000";
            OutputPostalCodeSearch output = await postalCodeSearch.ExecuteAsync(postalCodeNumber);
            Assert.True(output.IsSuccess);
            Assert.Equal("São Paulo", output.Localization);
            Assert.Equal("Praça da Sé", output.Street);
            Assert.Equal("Sé", output.Neighborhood);
        }

        [Fact]
        public async Task ExecuteAsync_ValidPostalCodeNumberRegisteredInDatabase_ShouldGetPostalCodeAddress()
        {
            Notifier notifier = new();
            string connectionString = _testsConfiguration.GetSection("ConnectionStrings:LibHouseBusiness").Value;
            LibHouseContext libHouseContext = new(new DbContextOptionsBuilder<LibHouseContext>().UseSqlServer(connectionString).Options);
            await libHouseContext.CleanContextDataAsync();
            await libHouseContext.Addresses.AddAsync(new Address("Praça da Sé", 99, "Sé", "São Paulo", "SP", "01001000", "lado ímpar"));
            await libHouseContext.SaveChangesAsync();
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
            string postalCodeNumber = "01001000";
            OutputPostalCodeSearch output = await postalCodeSearch.ExecuteAsync(postalCodeNumber);
            Assert.True(output.IsSuccess);
            Assert.Equal("São Paulo", output.Localization);
            Assert.Equal("Praça da Sé", output.Street);
            Assert.Equal("Sé", output.Neighborhood);
        }

        [Fact]
        public async Task ExecuteAsync_ValidPostalCodeNumberRegisteredInCache_ShouldGetPostalCodeAddress()
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
            await memoryCaching.GetOrCreateResourceAsync("01001000", TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(60), new Address("Praça da Sé", 99, "Sé", "São Paulo", "SP", "01001000", "lado ímpar"));
            IAddressRepository addressDecorator = new AddressMemoryCachingDecorator<IAddressRepository>(addressRepository, memoryCaching);
            ViaCepWebClient viaCepWebClient = new(new HttpClient(), new()
            {
                BaseUrl = "https://viacep.com.br/ws",
                EndpointResponseType = "json"
            });
            IAddressPostalCodeGateway addressPostalCodeGateway = new ViaCepAddressGateway(viaCepWebClient);
            PostalCodeSearch postalCodeSearch = new(notifier, addressDecorator, addressPostalCodeGateway);
            string postalCodeNumber = "01001000";
            OutputPostalCodeSearch output = await postalCodeSearch.ExecuteAsync(postalCodeNumber);
            Assert.True(output.IsSuccess);
            Assert.Equal("São Paulo", output.Localization);
            Assert.Equal("Praça da Sé", output.Street);
            Assert.Equal("Sé", output.Neighborhood);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidPostalCodeNumber_ShouldNotGetPostalCodeAddress()
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
            string postalCodeNumber = "01033300";
            OutputPostalCodeSearch output = await postalCodeSearch.ExecuteAsync(postalCodeNumber);
            Assert.False(output.IsSuccess);
        }
    }
}
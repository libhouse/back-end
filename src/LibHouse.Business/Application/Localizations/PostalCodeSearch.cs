using LibHouse.Business.Application.Localizations.Gateways;
using LibHouse.Business.Application.Localizations.Gateways.Outputs;
using LibHouse.Business.Application.Localizations.Interfaces;
using LibHouse.Business.Application.Localizations.Outputs;
using LibHouse.Business.Application.Localizations.Outputs.Converters;
using LibHouse.Business.Application.Shared;
using LibHouse.Business.Entities.Localizations;
using LibHouse.Business.Notifiers;
using System;
using System.Threading.Tasks;

namespace LibHouse.Business.Application.Localizations
{
    public class PostalCodeSearch : BaseUseCase, IPostalCodeSearch
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressPostalCodeGateway _addressPostalCodeGateway;

        public PostalCodeSearch(
            INotifier notifier, 
            IAddressRepository addressRepository,
            IAddressPostalCodeGateway addressPostalCodeGateway) 
            : base(notifier)
        {
            _addressRepository = addressRepository;
            _addressPostalCodeGateway = addressPostalCodeGateway;
        }

        public async Task<OutputPostalCodeSearch> ExecuteAsync(string postalCodeNumber)
        {
            if(string.IsNullOrEmpty(postalCodeNumber))
            {
                throw new ArgumentNullException(paramName: nameof(postalCodeNumber), "O código postal é obrigatório");
            }
            Address address = await _addressRepository.GetFirstAddressFromPostalCodeAsync(postalCodeNumber);
            if(address is not null)
            {
                return address.Convert();
            }
            OutputAddressPostalCodeGateway outputGateway = await _addressPostalCodeGateway.GetAddressByPostalCodeAsync(postalCodeNumber);
            if(!outputGateway.IsSuccess)
            {
                return new() { IsSuccess = false, PostalCodeSearchMessage = "Não foi possível localizar o código postal informado" };
            }
            return outputGateway.Convert();
        }
    }
}
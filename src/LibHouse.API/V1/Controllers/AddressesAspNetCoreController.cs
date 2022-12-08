using KissLog;
using LibHouse.API.Attributes.Authorization;
using LibHouse.API.BaseControllers;
using LibHouse.API.Filters.Swagger.Responses;
using LibHouse.API.Filters.Swagger.Responses.Addresses;
using LibHouse.Business.Application.Localizations.Interfaces;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Localizations;
using LibHouse.Infrastructure.Controllers.Http.Localizations.Adapters;
using LibHouse.Infrastructure.Controllers.Responses.Localizations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibHouse.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/addresses")]
    public class AddressesAspNetCoreController : MainController
    {
        private readonly AddressesWebApiAdapter _addressesWebApiAdapter;

        public AddressesAspNetCoreController(
            INotifier notifier, 
            ILoggedUser loggedUser, 
            IKLogger logger,
            IPostalCodeSearch postalCodeSearch) 
            : base(notifier, loggedUser, logger)
        {
            _addressesWebApiAdapter = new AddressesWebApiAdapter();
            _ = new AddressesController(_addressesWebApiAdapter, postalCodeSearch);
        }

        /// <summary>
        /// Busca os dados de um endereço a partir do seu número de código postal.
        /// </summary>
        /// <param name="postalCodeNumber" example="01001000">O número do código postal a ser pesquisado.</param>
        /// <returns>Em caso de sucesso, retorna um objeto com os dados do endereço. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">Os dados do endereço foram obtidos com sucesso.</response>
        /// <response code="404">O endereço não foi encontrado.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerResponseExample(200, typeof(PostalCodeSearchResponseExample))]
        [ProducesResponseType(typeof(PostalCodeSearchResponse), 200)]
        [SwaggerResponseExample(404, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 404)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [Authorize("User")]
        [HttpGet("postal-code/{postalCodeNumber}", Name = "Get Address By Postal Code")]
        public async Task<ActionResult<PostalCodeSearchResponse>> GetAddressByPostalCodeAsync(string postalCodeNumber)
        {
            Result<PostalCodeSearchResponse> postalCodeSearchResult = await _addressesWebApiAdapter.PostalCodeSearch(new() { PostalCodeNumber = postalCodeNumber });
            if (postalCodeSearchResult.Failure)
            {
                NotifyError("Busca do endereço por código postal", postalCodeSearchResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao buscar o código postal {postalCodeNumber}: {postalCodeSearchResult.Error}");
                return CustomResponseForGetEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Código postal {postalCodeNumber} localizado para o usuário {LoggedUser.GetUserEmail()}");
            return CustomResponseForGetEndpoint(postalCodeSearchResult.Value);
        }
    }
}
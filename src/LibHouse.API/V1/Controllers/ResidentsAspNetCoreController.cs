using KissLog;
using LibHouse.API.Attributes.Authorization;
using LibHouse.API.BaseControllers;
using LibHouse.API.Extensions.ModelState;
using LibHouse.API.Filters.Swagger.Requests.Residents;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Residents;
using LibHouse.Infrastructure.Controllers.Http.Residents.Adapters;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;

namespace LibHouse.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/residents")]
    public class ResidentsAspNetCoreController : MainController
    {
        private readonly ResidentsWebApiAdapter _residentsWebApiAdapter;

        public ResidentsAspNetCoreController(
            INotifier notifier,
            ILoggedUser loggedUser,
            IKLogger logger,
            IResidentRoomPreferencesRegistration residentRoomPreferencesRegistration,
            IResidentServicesPreferencesRegistration residentServicesPreferencesRegistration,
            IResidentChargePreferencesRegistration residentChargePreferencesRegistration,
            IResidentGeneralPreferencesRegistration residentGeneralPreferencesRegistration,
            IResidentLocalizationPreferencesRegistration residentLocalizationPreferencesRegistration) 
            : base(notifier, loggedUser, logger)
        {
            _residentsWebApiAdapter = new ResidentsWebApiAdapter();
            _ = new ResidentsController(
                _residentsWebApiAdapter, 
                residentRoomPreferencesRegistration,
                residentServicesPreferencesRegistration,
                residentChargePreferencesRegistration,
                residentGeneralPreferencesRegistration,
                residentLocalizationPreferencesRegistration
            );
        }

        /// <summary>
        /// Cadastra as preferências de cômodos de um morador.
        /// </summary>
        /// <param name="preferencesRegistrationViewModel">Objeto que possui os dados necessários para cadastrar as preferências de cômodos do morador.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">As preferências do morador foram registradas com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou as preferências já estão cadastradas.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [Authorize("Resident")]
        [HttpPost("register-room-preferences", Name = "Register Room Preferences")]
        public async Task<ActionResult> RegisterResidentRoomPreferencesAsync(ResidentRoomPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result preferencesRegistrationResult = await _residentsWebApiAdapter.ResidentRoomPreferencesRegistration(preferencesRegistrationViewModel);
            if (preferencesRegistrationResult.Failure)
            {
                NotifyError("Registro de preferências", preferencesRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar as preferências do morador: {preferencesRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Preferências de cômodo do morador {LoggedUser.GetUserEmail()} registradas com sucesso");
            return Ok();
        }

        /// <summary>
        /// Cadastra as preferências de serviços de um morador.
        /// </summary>
        /// <param name="preferencesRegistrationViewModel">Objeto que possui os dados necessários para cadastrar as preferências de serviços do morador.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">As preferências do morador foram registradas com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou as preferências já estão cadastradas.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [Authorize("Resident")]
        [HttpPost("register-services-preferences", Name = "Register Services Preferences")]
        public async Task<ActionResult> RegisterResidentServicesPreferencesAsync(ResidentServicesPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result preferencesRegistrationResult = await _residentsWebApiAdapter.ResidentServicesPreferencesRegistration(preferencesRegistrationViewModel);
            if (preferencesRegistrationResult.Failure)
            {
                NotifyError("Registro de preferências", preferencesRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar as preferências do morador: {preferencesRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Preferências de serviços do morador {LoggedUser.GetUserEmail()} registradas com sucesso");
            return Ok();
        }

        /// <summary>
        /// Cadastra as preferências de cobrança de um morador.
        /// </summary>
        /// <param name="preferencesRegistrationViewModel">Objeto que possui os dados necessários para cadastrar as preferências de cobranças do morador.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">As preferências do morador foram registradas com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou as preferências já estão cadastradas.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [Authorize("Resident")]
        [HttpPost("register-charge-preferences", Name = "Register Charge Preferences")]
        public async Task<ActionResult> RegisterResidentChargePreferencesAsync(ResidentChargePreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result preferencesRegistrationResult = await _residentsWebApiAdapter.ResidentChargePreferencesRegistration(preferencesRegistrationViewModel);
            if (preferencesRegistrationResult.Failure)
            {
                NotifyError("Registro de preferências", preferencesRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar as preferências do morador: {preferencesRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Preferências de cobrança do morador {LoggedUser.GetUserEmail()} registradas com sucesso");
            return Ok();
        }

        /// <summary>
        /// Cadastra as preferências gerais de um morador.
        /// </summary>
        /// <param name="preferencesRegistrationViewModel">Objeto que possui os dados necessários para cadastrar as preferências gerais do morador.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">As preferências do morador foram registradas com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou as preferências já estão cadastradas.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [Authorize("Resident")]
        [HttpPost("register-general-preferences", Name = "Register General Preferences")]
        public async Task<ActionResult> RegisterResidentGeneralPreferencesAsync(ResidentGeneralPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result preferencesRegistrationResult = await _residentsWebApiAdapter.ResidentGeneralPreferencesRegistration(preferencesRegistrationViewModel);
            if (preferencesRegistrationResult.Failure)
            {
                NotifyError("Registro de preferências", preferencesRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar as preferências do morador: {preferencesRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Preferências gerais do morador {LoggedUser.GetUserEmail()} registradas com sucesso");
            return Ok();
        }

        /// <summary>
        /// Cadastra as preferências de localização de um morador.
        /// </summary>
        /// <param name="preferencesRegistrationViewModel">Objeto que possui os dados necessários para cadastrar as preferências de localização do morador.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">As preferências do morador foram registradas com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou as preferências já estão cadastradas.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(ResidentLocalizationPreferencesRegistrationViewModel), typeof(ResidentLocalizationPreferencesRegistrationViewModelExample))]
        [Authorize("Resident")]
        [HttpPost("register-localization-preferences", Name = "Register Localization Preferences")]
        public async Task<ActionResult> RegisterResidentLocalizationPreferencesAsync(ResidentLocalizationPreferencesRegistrationViewModel preferencesRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result preferencesRegistrationResult = await _residentsWebApiAdapter.ResidentLocalizationPreferencesRegistration(preferencesRegistrationViewModel);
            if (preferencesRegistrationResult.Failure)
            {
                NotifyError("Registro de preferências", preferencesRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar as preferências do morador: {preferencesRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Preferências de localização do morador {LoggedUser.GetUserEmail()} registradas com sucesso");
            return Ok();
        }
    }
}
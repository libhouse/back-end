using KissLog;
using LibHouse.API.Attributes.Authorization;
using LibHouse.API.BaseControllers;
using LibHouse.API.Extensions.ModelState;
using LibHouse.Business.Application.Residents.Interfaces;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Residents;
using LibHouse.Infrastructure.Controllers.Http.Residents.Adapters;
using LibHouse.Infrastructure.Controllers.ViewModels.Residents;
using Microsoft.AspNetCore.Mvc;
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
            IResidentRoomPreferencesRegistration residentRoomPreferencesRegistration) 
            : base(notifier, loggedUser, logger)
        {
            _residentsWebApiAdapter = new ResidentsWebApiAdapter();
            _ = new ResidentsController(_residentsWebApiAdapter, residentRoomPreferencesRegistration);
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
            return Ok(preferencesRegistrationResult);
        }
    }
}
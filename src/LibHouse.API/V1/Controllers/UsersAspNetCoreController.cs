using KissLog;
using LibHouse.API.Attributes.Authorization;
using LibHouse.API.BaseControllers;
using LibHouse.API.Extensions.ModelState;
using LibHouse.API.Filters.Swagger.Requests.Users;
using LibHouse.API.Filters.Swagger.Responses;
using LibHouse.API.Filters.Swagger.Responses.Users;
using LibHouse.Business.Application.Users.Interfaces;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using LibHouse.Infrastructure.Controllers.Http.Users;
using LibHouse.Infrastructure.Controllers.Http.Users.Adapters;
using LibHouse.Infrastructure.Controllers.Responses.Users;
using LibHouse.Infrastructure.Controllers.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibHouse.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersAspNetCoreController : MainController
    {
        private readonly UsersWebApiAdapter _usersWebApiAdapter;

        public UsersAspNetCoreController(
            INotifier notifier,
            ILoggedUser loggedUser,
            IKLogger logger,
            IUserRegistration userRegistration,
            IConfirmUserRegistration confirmUserRegistration,
            IUserLogin userLogin,
            IUserLogout userLogout,
            IUserLoginRenewal userLoginRenewal,
            IUserPasswordReset userPasswordReset,
            IConfirmUserPasswordReset confirmUserPasswordReset)
            : base(
                  notifier,
                  loggedUser,
                  logger
            )
        {
            _usersWebApiAdapter = new UsersWebApiAdapter();
            _ = new UsersController(
                _usersWebApiAdapter,
                userRegistration,
                confirmUserRegistration,
                userLogin,
                userLogout,
                userLoginRenewal,
                userPasswordReset,
                confirmUserPasswordReset
            );
        }

        /// <summary>
        /// Cadastra um novo usuário na plataforma, enviando um e-mail automático de confirmação.
        /// </summary>
        /// <param name="userRegistrationViewModel">Objeto que possui os dados necessários para cadastrar o usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">O usuário foi registrado com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos, o usuário já está cadastrado ou houve uma falha para registrá-lo.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(UserRegistrationViewModel), typeof(UserRegistrationViewModelExample))]
        [SwaggerResponseExample(200, typeof(UserRegistrationResponseExample))]
        [ProducesResponseType(typeof(UserRegistrationResponse), 200)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [AllowAnonymous]
        [HttpPost("new-account", Name = "New Account")]
        public async Task<ActionResult<UserRegistrationResponse>> RegisterUserAsync(UserRegistrationViewModel userRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result<UserRegistrationResponse> userRegistrationResult = await _usersWebApiAdapter.UserRegistration(userRegistrationViewModel);
            if (userRegistrationResult.Failure)
            {
                NotifyError("Registro do usuário", userRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao realizar o registro do usuário: {userRegistrationResult.Error}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Usuário {userRegistrationViewModel.Email} registrado com sucesso");
            return Ok(userRegistrationResult.Value);
        }

        /// <summary>
        /// Confirma o cadastro de um novo usuário na plataforma.
        /// </summary>
        /// <param name="confirmUserRegistrationViewModel">Objeto que possui os dados necessários para confirmar o cadastro do usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="204">O cadastro do usuário foi confirmado com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou o token de confirmação expirou.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(ConfirmUserRegistrationViewModel), typeof(ConfirmUserRegistrationViewModelExample))]
        [ProducesResponseType(204)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [AllowAnonymous]
        [HttpPatch("confirm-registration", Name = "Confirm User Registration")]
        public async Task<ActionResult> ConfirmUserRegistrationAsync([FromBody] ConfirmUserRegistrationViewModel confirmUserRegistrationViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result confirmUserRegistrationResult = await _usersWebApiAdapter.ConfirmUserRegistration(confirmUserRegistrationViewModel);
            if (confirmUserRegistrationResult.Failure)
            {
                NotifyError("Confirmação do cadastro do usuário", confirmUserRegistrationResult.Error);
                Logger.Log(LogLevel.Error, $"Erro ao confirmar o cadastro do usuário: {confirmUserRegistrationResult.Error}");
                return CustomResponseForPatchEndpoint(confirmUserRegistrationResult);
            }
            Logger.Log(LogLevel.Information, $"Registro do usuário {confirmUserRegistrationViewModel.UserEmail} confirmado com sucesso");
            return CustomResponseForPatchEndpoint(confirmUserRegistrationResult);
        }

        /// <summary>
        /// Realiza o login de um usuário na plataforma, gerando um token de acesso.
        /// </summary>
        /// <param name="userLoginViewModel">Objeto que possui os dados necessários para confirmar o login do usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto com os dados do usuário e do token. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">O login do usuário foi confirmado com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou houve uma falha na autenticação do usuário.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(UserLoginViewModel), typeof(UserLoginViewModelExample))]
        [SwaggerResponseExample(200, typeof(UserLoginResponseExample))]
        [ProducesResponseType(typeof(UserLoginResponse), 200)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [AllowAnonymous]
        [HttpPost("login", Name = "User Login")]
        public async Task<ActionResult<UserLoginResponse>> LoginUserAsync(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result<UserLoginResponse> userLoginResult = await _usersWebApiAdapter.UserLogin(userLoginViewModel);
            if (userLoginResult.Failure)
            {
                NotifyError("Falha na autenticação do usuário", userLoginResult.Error);
                Logger.Log(LogLevel.Warning, userLoginResult.Error);
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Usuário {userLoginViewModel.Email} realizou login com sucesso: {DateTime.UtcNow}");
            return Ok(userLoginResult.Value);
        }

        /// <summary>
        /// Realiza o logout de um usuário.
        /// </summary>
        /// <param name="userLogoutViewModel">Objeto que possui os dados necessários para realizar o logout do usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="204">O logout do usuário foi confirmado com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou houve uma falha na revogação do refresh token do usuário.</response>
        /// <response code="401">O usuário que fez a requisição não está autenticado.</response>
        /// <response code="403">O usuário que fez a requisição não possui as permissões necessárias para acessar o recurso.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(UserLogoutViewModel), typeof(UserLogoutViewModelExample))]
        [ProducesResponseType(204)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [Authorize("User")]
        [HttpPatch("logout", Name = "Logout")]
        public async Task<ActionResult> LogoutUserAsync(UserLogoutViewModel userLogoutViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result userLogoutResult = await _usersWebApiAdapter.UserLogout(userLogoutViewModel);
            if (userLogoutResult.Failure)
            {
                NotifyError("Logout do usuário", userLogoutResult.Error);
                Logger.Log(LogLevel.Error, $"Erro no logout para o usuário {userLogoutViewModel.Email}: {userLogoutResult.Error}");
                return CustomResponseForPatchEndpoint(userLogoutResult);
            }
            Logger.Log(LogLevel.Information, $"Logout realizado para o usuário {userLogoutViewModel.Email}");
            return CustomResponseForPatchEndpoint(userLogoutResult);
        }

        /// <summary>
        /// Renova um access token expirado a partir de um refresh token.
        /// </summary>
        /// <param name="userLoginRenewalViewModel">Objeto que possui os dados necessários para renovar o access token.</param>
        /// <returns>Em caso de sucesso, retorna um objeto com os dados do usuário e do novo token. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">O token do usuário foi renovado com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou houve uma falha na validação do refresh token.</response>
        /// <response code="401">O usuário que fez a requisição não está autenticado.</response>
        /// <response code="403">O usuário que fez a requisição não possui as permissões necessárias para acessar o recurso.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(UserLoginRenewalViewModel), typeof(UserLoginRenewalViewModelExample))]
        [SwaggerResponseExample(200, typeof(UserLoginRenewalResponseExample))]
        [ProducesResponseType(typeof(UserLoginRenewalResponse), 200)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [Authorize("User")]
        [HttpPost("refresh-token", Name = "Refresh Token")]
        public async Task<ActionResult<UserLoginRenewalResponse>> RefreshTokenAsync(UserLoginRenewalViewModel userLoginRenewalViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result<UserLoginRenewalResponse> userLoginRenewalResult = await _usersWebApiAdapter.UserLoginRenewal(userLoginRenewalViewModel);
            if (userLoginRenewalResult.Failure)
            {
                NotifyError("Falhar ao renovar o access token", userLoginRenewalResult.Error);
                Logger.Log(LogLevel.Error, userLoginRenewalResult.Error);
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Usuário {userLoginRenewalViewModel.Email} renovou o seu login com sucesso: {DateTime.UtcNow}");
            return Ok(userLoginRenewalResult.Value);
        }

        /// <summary>
        /// Solicita a redefinição de senha para um usuário.
        /// </summary>
        /// <param name="userPasswordResetViewModel">Objeto que possui os dados necessários para solicitar a redefinição de senha do usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="200">A solicitação de redefinição de senha foi confirmada com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou houve uma falha na geração do token de redefinição de senha.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(UserPasswordResetViewModel), typeof(UserPasswordResetViewModelExample))]
        [SwaggerResponseExample(200, typeof(UserPasswordResetResponseExample))]
        [ProducesResponseType(typeof(UserPasswordResetResponse), 200)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [AllowAnonymous]
        [HttpPost("request-password-reset", Name = "Request Password Reset")]
        public async Task<ActionResult<UserPasswordResetResponse>> RequestPasswordResetAsync([FromBody] UserPasswordResetViewModel userPasswordResetViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result<UserPasswordResetResponse> userPasswordResetResult = await _usersWebApiAdapter.UserPasswordReset(userPasswordResetViewModel);
            if (userPasswordResetResult.Failure)
            {
                NotifyError("Solicitar redefinição de senha", userPasswordResetResult.Error);
                Logger.Log(LogLevel.Error, $"Falha ao criar o token de redefinição de senha para {LoggedUser.GetUserEmail()}");
                return CustomResponseForPostEndpoint();
            }
            Logger.Log(LogLevel.Information, $"Redefinição de senha solicitada para {LoggedUser.GetUserEmail()}");
            return Ok(userPasswordResetResult.Value);
        }

        /// <summary>
        /// Confirma a redefinição de senha de um usuário.
        /// </summary>
        /// <param name="confirmUserPasswordResetViewModel">Objeto que possui os dados necessários para confirmar a redefinição de senha do usuário.</param>
        /// <returns>Em caso de sucesso, retorna um objeto vazio. Em caso de erro, retorna uma lista de notificações.</returns>
        /// <response code="204">A senha do usuário foi redefinida com sucesso.</response>
        /// <response code="400">Os dados enviados são inválidos ou houve um erro na redefinição da senha.</response>
        /// <response code="500">Erro ao processar a requisição no servidor.</response>
        [SwaggerRequestExample(typeof(ConfirmUserPasswordResetViewModel), typeof(ConfirmUserPasswordResetViewModelExample))]
        [ProducesResponseType(204)]
        [SwaggerResponseExample(400, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 400)]
        [SwaggerResponseExample(500, typeof(NotificationResponseExample))]
        [ProducesResponseType(typeof(IEnumerable<Notification>), 500)]
        [AllowAnonymous]
        [HttpPatch("confirm-password-reset", Name = "Confirm Password Reset")]
        public async Task<ActionResult> ConfirmPasswordResetAsync([FromBody] ConfirmUserPasswordResetViewModel confirmUserPasswordResetViewModel)
        {
            if (ModelState.NotValid())
            {
                return CustomResponseFor(ModelState);
            }
            Result confirmUserPasswordResetResult = await _usersWebApiAdapter.ConfirmUserPasswordReset(confirmUserPasswordResetViewModel);
            if (confirmUserPasswordResetResult.Failure)
            {
                NotifyError("Aceitar redefinição de senha do usuário", confirmUserPasswordResetResult.Error);
                Logger.Log(LogLevel.Error, $"Erro ao redefinir a senha do usuário {LoggedUser.GetUserEmail()}: {confirmUserPasswordResetResult.Error}");
                return CustomResponseForPatchEndpoint(confirmUserPasswordResetResult);
            }
            Logger.Log(LogLevel.Information, $"Senha redefinida para o usuário {LoggedUser.GetUserEmail()}");
            return CustomResponseForPatchEndpoint(confirmUserPasswordResetResult);
        }
    }
}
using KissLog;
using LibHouse.Business.Monads;
using LibHouse.Business.Notifiers;
using LibHouse.Infrastructure.Authentication.Login.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace LibHouse.API.BaseControllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        public ILoggedUser LoggedUser { get; }
        public IKLogger Logger { get; }

        protected MainController(
            INotifier notifier,
            ILoggedUser loggedUser,
            IKLogger logger)
        {
            _notifier = notifier;
            LoggedUser = loggedUser;
            Logger = logger;
        }

        protected bool EndpointOperationWasSuccessful => !_notifier.HasNotification();
        protected bool EndpointOperationFailed => !EndpointOperationWasSuccessful;

        protected void NotifyError(string errorTitle, string errorMessage) =>
            _notifier.Handle(new Notification(errorMessage, errorTitle));

        protected ActionResult CustomResponseForGetEndpoint(object response = null)
        {
            if (response is null)
            {
                return NotFound(_notifier.GetNotifications());
            }

            return CustomResponse(response);
        }

        protected ActionResult CustomResponseForPostEndpoint(
            object response = null,
            object resourceIdentifier = null,
            string resourceCreatedAt = null)
        {
            if (EndpointOperationFailed)
            {
                return BadRequest(_notifier.GetNotifications());
            }

            return CreatedAtAction(resourceCreatedAt, new { id = resourceIdentifier }, response);
        }

        protected ActionResult CustomResponseForPatchEndpoint(Result result)
        {
            if (EndpointOperationWasSuccessful && result.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(_notifier.GetNotifications());
        }

        protected ActionResult CustomResponseFor(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotifyErrorsForInvalidModelState(modelState);
            }

            return CustomResponse();
        }

        private ActionResult CustomResponse(object response = null)
        {
            if (EndpointOperationWasSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(_notifier.GetNotifications());
        }

        private void NotifyErrorsForInvalidModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(m => m.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

                var errorTitle = error.Exception.Source;

                NotifyError(errorTitle, errorMessage);
            }
        }
    }
}
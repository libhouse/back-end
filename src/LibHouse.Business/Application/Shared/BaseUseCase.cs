using FluentValidation;
using FluentValidation.Results;
using LibHouse.Business.Entities.Shared;
using LibHouse.Business.Notifiers;

namespace LibHouse.Business.Application.Shared
{
    public abstract class BaseUseCase
    {
        private readonly INotifier _notifier;

        protected BaseUseCase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        protected void Notify(string title, string message)
        {
            _notifier.Handle(new Notification(message, title));
        }

        protected bool ExecuteValidation<TV, TE>(TV validator, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validation = validator.Validate(entity);
            if (validation.IsValid) return true;
            Notify(validation);
            return false;
        }
    }
}
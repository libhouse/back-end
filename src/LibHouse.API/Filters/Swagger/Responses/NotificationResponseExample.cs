using LibHouse.Business.Notifiers;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace LibHouse.API.Filters.Swagger.Responses
{
    public class NotificationResponseExample : IExamplesProvider<IEnumerable<Notification>>
    {
        public IEnumerable<Notification> GetExamples()
        {
            return new List<Notification>()
            {
                new(message: "Detalhes da notificação", title: "Título da notificação")
            };
        }
    }
}
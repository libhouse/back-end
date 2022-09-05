using LibHouse.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace LibHouse.API.Providers.Versioning
{
    public class ApiVersioningErrorResponseProvider : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            var apiVersioningError = new ProblemDetails()
            {
                Status = context.StatusCode,
                Instance = context.Request.Path.Value,
                Detail = context.MessageDetail,
                Title = context.Message
            };

            var response = new ObjectResult(apiVersioningError)
            {
                StatusCode = context.StatusCode,
                ContentTypes = ObjectResultHelper.BuildMediaTypeCollectionWith("application/problem+json")
            };

            return response;
        }
    }
}
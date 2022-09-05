using LibHouse.API.Extensions.Http;
using LibHouse.Infrastructure.Authentication.Extensions.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibHouse.API.Attributes.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly IList<string> _roles;

        public AuthorizeAttribute(params string[] roles)
        {
            _roles = roles ?? Array.Empty<string>();
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous) return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);

                return;
            }

            if (await context.HttpContext.CheckIfUserAccessTokenIsRevokedAsync())
            {
                context.Result = new StatusCodeResult(401);

                return;
            }

            if (!context.HttpContext.User.CheckIfUserHasOneOfTheseRoles(_roles))
            {
                context.Result = new StatusCodeResult(403);

                return;
            }           
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuctionApp.API.Filters
{
    public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = TokenOnRequest(context.HttpContext);
        }

        private string TokenOnRequest(HttpContext context)
        {
            var auth = context.Request.Headers.Authorization.ToString();

            if (string.IsNullOrEmpty(auth)) 
            {
                throw new Exception("Token is missing.");
            }

            return auth["Bearer ".Length..];
        }
    }
}

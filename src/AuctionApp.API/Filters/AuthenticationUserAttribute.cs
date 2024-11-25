using AuctionApp.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuctionApp.API.Filters
{
    public class AuthenticationUserAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenOnRequest(context.HttpContext);
                var repository = new AuctionAppDbContext();

                var email = FromBase64String(token);

                var exist = repository.Users.Any(user => user.Email.Equals(email));

                if (!exist)
                {
                    context.Result = new UnauthorizedObjectResult("Email not valid");
                }
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedObjectResult(ex.Message);
            }
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

        private string FromBase64String(string base64)
        {
            var data = Convert.FromBase64String(base64);

            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}

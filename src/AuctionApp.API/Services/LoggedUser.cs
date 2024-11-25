using AuctionApp.API.Entities;
using AuctionApp.API.Repositories;
using Microsoft.AspNetCore.Http;

namespace AuctionApp.API.Services
{
    public class LoggedUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public LoggedUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        
        public User User()
        {
            var repository = new AuctionAppDbContext();

            var token = TokenOnRequest();
            var email = FromBase64String(token);

            return repository.Users.First(user => user.Email.Equals(email));
        }

        private string TokenOnRequest()
        {
            var auth = _httpContext.HttpContext!.Request.Headers.Authorization.ToString();

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

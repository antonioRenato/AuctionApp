using AuctionApp.API.Contracts;
using AuctionApp.API.Entities;
using AuctionApp.API.Repositories;

namespace AuctionApp.API.Services
{
    public class LoggedUser
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _repository;

        public LoggedUser(IHttpContextAccessor httpContext, IUserRepository repository)
        {
            _httpContext = httpContext;
            _repository = repository;
        }
        
        public User User()
        {
            var token = TokenOnRequest();
            var email = FromBase64String(token);

            return _repository.GetUserByEmail(email);
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

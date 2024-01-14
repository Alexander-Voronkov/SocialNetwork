using Application.Users.Commands.CreateUser;
using IdentityModel.Client;
using Infrastructure;
using MediatR;
using SocialNetworkApi.Data;
using Newtonsoft.Json;

namespace SocialNetworkApi.Middlewares
{
    public class CreateUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CreateUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var dbcontext = context
                        .RequestServices
                        .GetRequiredService<ApplicationDbContext>();

            var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";

            var authapihost = Environment.GetEnvironmentVariable("AUTH_API_CONTAINER_NAME") ?? "localhost";

            var authapiport = Environment.GetEnvironmentVariable("AUTH_API_PORT") ?? "7006";

            var _sender = context
                        .RequestServices
                        .GetRequiredService<ISender>();
            var client = new HttpClient();
            var rawToken = context.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(rawToken) && !string.IsNullOrWhiteSpace(rawToken))
            {
                var token = rawToken.ToString().Split(' ')[1];
                var userData = await client.GetUserInfoAsync(new UserInfoRequest()
                {
                    Token = token,
                    Address = $"{protocol}://{authapihost}:{authapiport}/connect/userinfo"
                });

                var decodedData = JsonConvert.DeserializeObject<UserInfoEndpointResult>(userData.Json.ToString());
                
                if (decodedData != null && await dbcontext.Users.FindAsync(decodedData.Sub) == null)
                {
                    var createdUserId = await _sender.Send(new CreateUserCommand
                    {
                        Id = decodedData.Sub,
                        Email = decodedData.Email,
                        PhoneNumber = decodedData.Phone,
                        Username = decodedData.Username,
                        EmailConfirmed = decodedData.EmailConfirmed,
                        PhoneConfirmed = decodedData.PhoneConfirmed,
                    });
                }
            }

            await _next(context);
        }
    }
}

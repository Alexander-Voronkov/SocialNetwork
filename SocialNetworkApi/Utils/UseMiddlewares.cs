using SocialNetworkApi.Middlewares;

namespace SocialNetworkApi.Utils
{
    public static partial class Utils
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<CreateUserMiddleware>();

            return app;
        }
    }
}

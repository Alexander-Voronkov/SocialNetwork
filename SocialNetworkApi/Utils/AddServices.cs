using Application.Common.Interfaces;
using SocialNetworkApi.Services;

namespace SocialNetworkApi.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<IUser, CurrentUser>();

            return services;
        }
    }
}

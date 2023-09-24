using UIApp.Services.Interfaces;
using UIApp.Services.Realizations;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUser, CurrentUser>();
            services.AddSingleton<IWatchingChatCacheRepository, WatchingChatCache>();
            services.AddSingleton<IWatchingPostCacheRepository, WatchingPostCache>();
            return services;
        }
    }
}

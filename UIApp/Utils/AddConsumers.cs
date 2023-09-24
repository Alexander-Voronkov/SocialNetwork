using UIApp.Services.Realizations;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddSingleton<RabbitFriendrequestNotificationConsumer>();
            services.AddSingleton<RabbitCommentNotificationConsumer>();
            services.AddSingleton<RabbitFriendshipNotificationConsumer>();
            services.AddHostedService<RabbitMessageNotificationConsumer>();
            services.AddHostedService<RabbitPostNotificationConsumer>();
            services.AddHostedService<RabbitReactionNotificationConsumer>();
            return services;
        }
    }
}

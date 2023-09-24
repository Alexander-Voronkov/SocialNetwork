using UIApp.Configuration;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddRabbitMqConfigurations(this IServiceCollection services)
        {

            // queue names 

            string CreatedMessageEventQueue = Environment.GetEnvironmentVariable("CreatedMessageEventQueue") ?? "created-message-event-queue";
            string CreatedChatEventQueue = Environment.GetEnvironmentVariable("CreatedChatEventQueue") ?? "created-chat-event-queue";
            string CreatedCommentEventQueue = Environment.GetEnvironmentVariable("CreatedCommentEventQueue") ?? "created-comment-event-queue";
            string CreatedFriendrequestEventQueue = Environment.GetEnvironmentVariable("CreatedFriendrequestEventQueue") ?? "created-friendrequest-event-queue";
            string CreatedFriendshipEventQueue = Environment.GetEnvironmentVariable("CreatedFriendshipEventQueue") ?? "created-friendship-event-queue";
            string CreatedReactionEventQueue = Environment.GetEnvironmentVariable("CreatedReactionEventQueue") ?? "created-reaction-event-queue";
            string UpdatedPostEventQueue = Environment.GetEnvironmentVariable("UpdatedPostEventQueue") ?? "updated-post-event-queue";
            string UpdatedMessageEventQueue = Environment.GetEnvironmentVariable("UpdatedMessageEventQueue") ?? "updated-message-event-queue";
            string RemovedMessageEventQueue = Environment.GetEnvironmentVariable("RemovedMessageEventQueue") ?? "removed-message-event-queue";
            string RemovedReactionEventQueue = Environment.GetEnvironmentVariable("RemovedReactionEventQueue") ?? "removed-reaction-event-queue";


            services.Configure<QueueNamesConfiguration>(options =>
            {
                options.CreatedFriendrequestEventQueue = CreatedFriendrequestEventQueue;
                options.CreatedChatEventQueue = CreatedChatEventQueue;
                options.CreatedMessageEventQueue = CreatedMessageEventQueue;
                options.CreatedCommentEventQueue = CreatedCommentEventQueue;
                options.CreatedReactionEventQueue = CreatedReactionEventQueue;
                options.CreatedFriendshipEventQueue = CreatedFriendshipEventQueue;
                options.UpdatedMessageEventQueue = UpdatedMessageEventQueue;
                options.UpdatedPostEventQueue = UpdatedPostEventQueue;
                options.RemovedReactionEventQueue = RemovedReactionEventQueue;
                options.RemovedMessageEventQueue = RemovedMessageEventQueue;
            });

            // rabbit mq connection info

            string rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
            string ? port = Environment.GetEnvironmentVariable("RABBITMQ_PORT");
            int rabbitPort = port == null ? 5672 : int.Parse(port);
            string rabbitUsername = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER") ?? "ADMIN";
            string rabbitPassword = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS") ?? "Admin_1234";
            
            services.Configure<RabbitMQConfiguration>(options =>
            {
                options.Host = rabbitHost;
                options.Username = rabbitUsername;
                options.Port = rabbitPort;
                options.Password = rabbitPassword;
            });

            return services;
        }
    }
}

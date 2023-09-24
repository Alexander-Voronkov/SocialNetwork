using Infrastructure.Configuration;

namespace SocialNetworkApi.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddRabbitMqConfiguration(this IServiceCollection services)
        {
            var rabbitHost = Environment.GetEnvironmentVariable("RabbitMQHost") ?? "localhost";
            var rabbitPort = Environment.GetEnvironmentVariable("RabbitMQPort") ?? "5672";
            var rabbitUser = Environment.GetEnvironmentVariable("RabbitMQUser") ?? "ADMIN";
            var rabbitPass = Environment.GetEnvironmentVariable("RabbitMQPass") ?? "Admin_1234";

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

            services.Configure<RabbitMQConfiguration>(options =>
            {
                options.Host = rabbitHost;
                options.Port = int.Parse(rabbitPort!);
                options.Username = rabbitUser;
                options.Password = rabbitPass;
            });


            return services;
        }
    }
}

using Hangfire;
using UIApp.Services.Realizations;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static void AddJobs()
        {
            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                "posts",
                consumer => consumer.Consume(
                    "social-network-created-post-event",
                    new SocialNetworkPostProcessor()),
                Cron.Minutely());

            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                "users",
                consumer => consumer.Consume(
                    "social-network-created-user-event",
                    new SocialNetworkUserProcessor()),
                Cron.Minutely());

            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                "reactions",
                consumer => consumer.Consume(
                    "social-network-created-reaction-event",
                    new SocialNetworkReactionProcessor()),
                Cron.Minutely());
        }
    }
}

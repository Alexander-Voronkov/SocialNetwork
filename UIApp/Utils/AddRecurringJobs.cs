using Hangfire;
using UIApp.Services.Realizations;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static void AddJobs()
        {
            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                consumer => consumer.Consume(
                    "social-network-created-post-event",
                    new SocialNetworkPostProcessor()),
                Cron.Minutely());

            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                consumer => consumer.Consume(
                    "social-network-created-user-event",
                    new SocialNetworkUserProcessor()),
                Cron.Minutely());

            RecurringJob.AddOrUpdate<RabbitQueueConsumer>(
                consumer => consumer.Consume(
                    "social-network-created-reaction-event",
                    new SocialNetworkReactionProcessor()),
                Cron.Minutely());
        }
    }
}

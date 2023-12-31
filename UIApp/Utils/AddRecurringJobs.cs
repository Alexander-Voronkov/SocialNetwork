﻿using Hangfire;
using UIApp.Services.Realizations;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static void AddJobs()
        {
            RecurringJob.AddOrUpdate<RabbitCommentNotificationConsumer>(
                "comments",
                consumer => consumer.Consume(CancellationToken.None),
                "* * * ? * *");

            RecurringJob.AddOrUpdate<RabbitFriendrequestNotificationConsumer>(
                "friendrequests",
                consumer => consumer.Consume(CancellationToken.None),
                "* * * ? * *");

            RecurringJob.AddOrUpdate<RabbitFriendshipNotificationConsumer>(
                "friendships",
                consumer => consumer.Consume(CancellationToken.None),
                "* * * ? * *");
        }
    }
}

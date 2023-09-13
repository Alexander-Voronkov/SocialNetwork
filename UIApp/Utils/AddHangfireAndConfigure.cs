using Hangfire;
using Microsoft.Data.SqlClient;
using Shared;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static async Task<IServiceCollection> AddHangfireAndConfigure(this IServiceCollection services)
        {
            var dbHangfireHost = Environment.GetEnvironmentVariable("DB_HANGFIRE_HOST") ?? "127.0.0.1";
            var dbHangfirePort = Environment.GetEnvironmentVariable("DB_HANGFIRE_PORT") ?? "1444";
            var dbHangfireName = Environment.GetEnvironmentVariable("DB_HANGFIRE_NAME") ?? "DB_HANGFIRE_JOBS";
            var dbHangfireUser = Environment.GetEnvironmentVariable("DB_HANGFIRE_USERID") ?? "sa";
            var dbHangfirePassword = Environment.GetEnvironmentVariable("DB_HANGFIRE_PASS") ?? "Admin_1234";

            var sqlbuilder = new SqlConnectionStringBuilder();
            sqlbuilder.DataSource = $"{dbHangfireHost},{dbHangfirePort}";
            sqlbuilder.InitialCatalog = $"{dbHangfireName}";
            sqlbuilder.UserID = $"{dbHangfireUser}";
            sqlbuilder.Password = $"{dbHangfirePassword}";
            sqlbuilder.Encrypt = true;
            sqlbuilder.TrustServerCertificate = true;

            string connStr = sqlbuilder.ConnectionString;

            // rabbit mq connection info

            services.Configure<RabbitMQConfiguration>(options =>
            {
                options.Host = Environment.GetEnvironmentVariable("RabbitMQHost") ?? "127.0.0.1";
                string? port = Environment.GetEnvironmentVariable("RabbitMQPort");
                options.Port = port == null ? 5672 : int.Parse(port);
                options.Username = Environment.GetEnvironmentVariable("RabbitMQUser") ?? "ADMIN";
                options.Password = Environment.GetEnvironmentVariable("RabbitMQPass") ?? "Admin_1234";
            });

            services.AddHangfire(config =>
            {
                config
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connStr);
            });


            await Utils.InitHangfireDb(sqlbuilder);

            return services;

        }
    }
}

using Shared;
using Application;

namespace SocialNetworkApi.Utils
{
    public static partial class ServicesExtensions
    {
        public static IServiceCollection AddLayersAndConfigure(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DBAPI_HOST") ?? "127.0.0.1";
            var dbPort = Environment.GetEnvironmentVariable("DBAPI_PORT") ?? "1433";
            var dbName = Environment.GetEnvironmentVariable("DBAPI_NAME") ?? "SOCIALNETWORK";
            var dbUser = Environment.GetEnvironmentVariable("DBAPI_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("DBAPI_PASS") ?? "Admin_1234";
            var rabbitHost = Environment.GetEnvironmentVariable("RabbitMQHost") ?? "127.0.0.1";
            var rabbitPort = Environment.GetEnvironmentVariable("RabbitMQPort") ?? "5672";
            var rabbitUser = Environment.GetEnvironmentVariable("RabbitMQUser") ?? "ADMIN";
            var rabbitPass = Environment.GetEnvironmentVariable("RabbitMQPass") ?? "Admin_1234";
            var identityDbHost = Environment.GetEnvironmentVariable("IDENTITY_HOST") ?? "127.0.0.1";
            var identityDbPort = Environment.GetEnvironmentVariable("IDENTITY_PORT") ?? "1422";
            var identityDbName = Environment.GetEnvironmentVariable("IDENTITY_NAME") ?? "USERS_IDENTITY_DB";
            var identityDbUser = Environment.GetEnvironmentVariable("IDENTITY_USERID") ?? "sa";
            var identityDbPassword = Environment.GetEnvironmentVariable("IDENTITY_PASS") ?? "Admin_1234";

            string connStr =
                    $"Data Source={dbHost},{dbPort};" +
                    $"Initial Catalog={dbName};" +
                    $"User ID={dbUser};Password={dbPassword}";

            string identityConnStr =
                    $"Data Source={identityDbHost},{identityDbPort};" +
                    $"Initial Catalog={identityDbName};" +
                    $"User ID={identityDbUser};Password={identityDbPassword}";

            services.Configure<RabbitMQConfiguration>(options =>
            {
                options.Host = rabbitHost;
                options.Port = int.Parse(rabbitPort!);
                options.Username = rabbitUser;
                options.Password = rabbitPass;
            });

            services
                .AddApplication()
                .AddInfrastructure(connStr);

            return services;
        }
    }
}

using Application;
using Infrastructure.Configuration;

namespace SocialNetworkApi.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddLayersAndConfigure(this IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DBAPI_HOST") ?? "localhost";
            var dbPort = Environment.GetEnvironmentVariable("DBAPI_PORT") ?? "1433";
            var dbName = Environment.GetEnvironmentVariable("DBAPI_NAME") ?? "SOCIALNETWORK";
            var dbUser = Environment.GetEnvironmentVariable("DBAPI_USERID") ?? "sa";
            var dbPassword = Environment.GetEnvironmentVariable("DBAPI_PASS") ?? "Admin_1234";
            

            string connStr =
                    $"Data Source={dbHost},{dbPort};" +
                    $"Initial Catalog={dbName};" +
                    $"User ID={dbUser};Password={dbPassword}";

            services.Configure<MainDatabaseConfiguration>(options =>
            {
                options.Name = dbName;
                options.User = dbUser;
                options.Host = dbHost;
                options.Port = dbPort;
                options.Pass = dbPassword;
            });           

            services
                .AddApplication()
                .AddInfrastructure(connStr);

            return services;
        }
    }
}

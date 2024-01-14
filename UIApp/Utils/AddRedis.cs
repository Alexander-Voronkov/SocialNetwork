using UIApp.Configuration;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services)
        {
            string redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
            string? port = Environment.GetEnvironmentVariable("REDIS_PORT");
            int redisPort = port == null ? 6379 : int.Parse(port);
            string redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? "Admin_1234";

            services.Configure<RedisCacheConfiguration>(options =>
            {
                options.Host = redisHost;
                options.Port = redisPort;
                options.Password = redisPassword;
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                {
                    Password = redisPassword,
                    EndPoints = { { redisHost, redisPort } },
                };
            });

            return services;
        }
    }
}

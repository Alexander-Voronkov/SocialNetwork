using Hangfire;
using Microsoft.Data.SqlClient;
using UIApp.Configuration;

namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHangfireAndConfiguration(this IServiceCollection services)
        {
            var dbHangfireHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            var dbHangfirePort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
            var dbHangfireName = Environment.GetEnvironmentVariable("HANGFIRE_DB_NAME") ?? "DB_HANGFIRE_JOBS";
            var dbHangfireUser = Environment.GetEnvironmentVariable("DB_USERID") ?? "sa";
            var dbHangfirePassword = Environment.GetEnvironmentVariable("DB_PASS") ?? "Admin_1234";

            services.Configure<HangfireDbConfiguration>(options =>
            {
                options.HangfireHost = dbHangfireHost;
                options.HangfirePort = dbHangfirePort;
                options.HangfireName = dbHangfireName;
                options.HangfirePass = dbHangfirePassword;
                options.HangfireUser = dbHangfireUser;
            });
            
            var sqlbuilder = new SqlConnectionStringBuilder();
            sqlbuilder.DataSource = $"{dbHangfireHost},{dbHangfirePort}";
            sqlbuilder.InitialCatalog = $"{dbHangfireName}";
            sqlbuilder.UserID = $"{dbHangfireUser}";
            sqlbuilder.Password = $"{dbHangfirePassword}";
            sqlbuilder.Encrypt = false;

            string connStr = sqlbuilder.ConnectionString;

            services.AddHangfire(config =>
            {
                config
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(connStr);
            });

            services.AddHangfireServer();

            return services;
        }
    }
}

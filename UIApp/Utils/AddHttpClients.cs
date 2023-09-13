namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("DataApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129");
            });

            return services;
        }
    }
}

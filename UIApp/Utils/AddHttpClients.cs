namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("UsersApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/users/");
            });

            services.AddHttpClient("ChatsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/chats/");
            });

            services.AddHttpClient("CommentsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/comments/");
            });

            services.AddHttpClient("FriendrequestsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/friendrequests/");
            });

            services.AddHttpClient("FriendshipsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/friendships/");
            });

            services.AddHttpClient("PostsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/posts/");
            });

            services.AddHttpClient("ReactionsApi", (services, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:7129/api/reactions/");
            });

            return services;
        }
    }
}

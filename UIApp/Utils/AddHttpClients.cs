namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var webapihost = $"{Environment.GetEnvironmentVariable("WEB_API_CONTAINER_NAME") ?? "localhost"}:{Environment.GetEnvironmentVariable("WEB_API_PORT") ?? "7129"}";

            var protocol = Environment.GetEnvironmentVariable("PROTOCOL") ?? "http";

            services.AddUserAccessTokenHttpClient("UsersApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/users/");
            });

            services.AddUserAccessTokenHttpClient("ChatsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/chats/");
            });

            services.AddUserAccessTokenHttpClient("CommentsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/comments/");
            });

            services.AddUserAccessTokenHttpClient("FriendrequestsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/friendrequests/");
            });

            services.AddUserAccessTokenHttpClient("FriendshipsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/friendships/");
            });

            services.AddUserAccessTokenHttpClient("PostsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/posts/");
            });

            services.AddUserAccessTokenHttpClient("ReactionsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/reactions/");
            });

            services.AddUserAccessTokenHttpClient("MessagesApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{protocol}://{webapihost}/api/messages/");
            });

            return services;
        }
    }
}

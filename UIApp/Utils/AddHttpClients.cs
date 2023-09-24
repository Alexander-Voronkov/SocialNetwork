namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var webapihost = (Environment.GetEnvironmentVariable("WEB_API_HOST") ?? "localhost") 
                + ":" + (Environment.GetEnvironmentVariable("WebApiPort") ?? "7129");

            services.AddUserAccessTokenHttpClient("UsersApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/users/");
            });

            services.AddUserAccessTokenHttpClient("ChatsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/chats/");
            });

            services.AddUserAccessTokenHttpClient("CommentsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/comments/");
            });

            services.AddUserAccessTokenHttpClient("FriendrequestsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/friendrequests/");
            });

            services.AddUserAccessTokenHttpClient("FriendshipsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/friendships/");
            });

            services.AddUserAccessTokenHttpClient("PostsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/posts/");
            });

            services.AddUserAccessTokenHttpClient("ReactionsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/reactions/");
            });

            services.AddUserAccessTokenHttpClient("MessagesApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"https://{webapihost}/api/messages/");
            });

            return services;
        }
    }
}

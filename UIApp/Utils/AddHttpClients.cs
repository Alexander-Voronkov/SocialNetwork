namespace UIApp.Utils
{
    public static partial class Utils
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var webapihost = $"{Environment.GetEnvironmentVariable("NGINX_WEB_API_HOST") ?? "localhost"}:{Environment.GetEnvironmentVariable("NGINX_WEBAPI_INNER_PORT") ?? "8081"}";

            var nginxprotocol = Environment.GetEnvironmentVariable("NGINX_PROTOCOL") ?? "http";

            services.AddUserAccessTokenHttpClient("UsersApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/users/");
            });

            services.AddUserAccessTokenHttpClient("ChatsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/chats/");
            });

            services.AddUserAccessTokenHttpClient("CommentsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/comments/");
            });

            services.AddUserAccessTokenHttpClient("FriendrequestsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/friendrequests/");
            });

            services.AddUserAccessTokenHttpClient("FriendshipsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/friendships/");
            });

            services.AddUserAccessTokenHttpClient("PostsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/posts/");
            });

            services.AddUserAccessTokenHttpClient("ReactionsApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/reactions/");
            });

            services.AddUserAccessTokenHttpClient("MessagesApi", configureClient: client =>
            {
                client.BaseAddress = new Uri($"{nginxprotocol}://{webapihost}/api/messages/");
            });

            return services;
        }
    }
}

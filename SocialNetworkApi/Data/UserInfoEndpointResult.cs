using Newtonsoft.Json;

namespace SocialNetworkApi.Data
{
    public class UserInfoEndpointResult
    {
        [JsonProperty("sub")]
        public int Sub { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone_number")]
        public string Phone { get; set; }

        [JsonProperty("name")]
        public string Username { get; set; }

        [JsonProperty("email_confirmed")]
        public bool EmailConfirmed { get; set; }
        [JsonProperty("phone_number_confirmed")]
        public bool PhoneConfirmed { get; set; }
    }
}

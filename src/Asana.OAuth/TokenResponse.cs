using Newtonsoft.Json;

namespace Asana.OAuth
{
    public sealed class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; }
        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; }
        [JsonProperty("data")]
        public User AuthenticatedUser { get; }
        [JsonProperty("token_type")]
        public string TokenType { get; }

        [JsonConstructor]
        public TokenResponse(string accessToken, int expiresInSeconds, string refreshToken, User authenticatedUser, string tokenType)
        {
            AccessToken = accessToken;
            ExpiresInSeconds = expiresInSeconds;
            RefreshToken = refreshToken;
            AuthenticatedUser = authenticatedUser;
            TokenType = tokenType;
        }

        public sealed class User
        {
            public long Id { get; }
            public string Gid { get; }
            public string Name { get; }
            public string Email { get; }

            [JsonConstructor]
            public User(long id, string gid, string name, string email)
            {
                Id = id;
                Gid = gid;
                Name = name;
                Email = email;
            }
        }
    }
}
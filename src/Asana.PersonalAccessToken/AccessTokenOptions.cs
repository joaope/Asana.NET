namespace Asana.PersonalAccessToken
{
    public sealed class AccessTokenOptions
    {
        public string AccessToken { get; }

        public AccessTokenOptions(string accessToken)
        {
            AccessToken = accessToken;
        }

        public static implicit operator AccessTokenOptions(string accessToken)
        {
            return new AccessTokenOptions(accessToken);
        }

        public static implicit operator string(AccessTokenOptions accessTokenOptions)
        {
            return accessTokenOptions.AccessToken;
        }
    }
}
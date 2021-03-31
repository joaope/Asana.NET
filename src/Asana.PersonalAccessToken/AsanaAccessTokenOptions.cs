namespace Asana.PersonalAccessToken
{
    public sealed class AsanaAccessTokenOptions
    {
        public string AccessToken { get; }

        public AsanaAccessTokenOptions(string accessToken)
        {
            AccessToken = accessToken;
        }

        public static implicit operator AsanaAccessTokenOptions(string accessToken)
        {
            return new AsanaAccessTokenOptions(accessToken);
        }

        public static implicit operator string(AsanaAccessTokenOptions asanaAccessTokenOptions)
        {
            return asanaAccessTokenOptions.AccessToken;
        }
    }
}
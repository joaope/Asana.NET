namespace Asana.PersonalAccessToken
{
    public sealed class AsanaAccessTokenOptions
    {
        public string AccessToken { get; }

        public AsanaAccessTokenOptions(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
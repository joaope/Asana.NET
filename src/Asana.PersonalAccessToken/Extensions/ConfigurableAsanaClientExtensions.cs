namespace Asana.PersonalAccessToken.Extensions
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithPersonalAccessToken(
            this IConfigurableAsanaClient configurableAsanaClient,
            string accessToken)
        {
            return configurableAsanaClient
                .WithDispatcher(new AsanaAccessTokenDispatcher(new AsanaAccessTokenOptions(accessToken), configurableAsanaClient.Options));
        }

        public static IAsanaClient WithPersonalAccessToken(
            this IConfigurableAsanaClient configurableAsanaClient,
            AsanaAccessTokenOptions accessTokenOptions)
        {
            return configurableAsanaClient
                .WithDispatcher(new AsanaAccessTokenDispatcher(accessTokenOptions, configurableAsanaClient.Options));
        }
    }
}
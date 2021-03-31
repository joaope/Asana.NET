namespace Asana.PersonalAccessToken.Extensions
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithPersonalAccessToken(
            this IConfigurableAsanaClient configurableAsanaClient,
            string accessToken)
        {
            return configurableAsanaClient
                .WithDispatcher(new AsanaAccessTokenDispatcher(accessToken, configurableAsanaClient.Options));
        }
    }
}
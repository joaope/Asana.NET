namespace Asana.PersonalAccessToken
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithPersonalAccessToken(
            this IConfigurableAsanaClient configurableAsanaClient,
            string accessToken)
        {
            return configurableAsanaClient
                .WithDispatcher(new AccessTokenDispatcher(accessToken, configurableAsanaClient.Options));
        }
    }
}
namespace Asana.OAuth.Extensions
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithOAuth(
            this IConfigurableAsanaClient configurableAsanaClient,
            OAuthApplicationOptions oAuthApplicationOptions)
        {
            var oAuthApplication = new AsanaOAuthApplication(oAuthApplicationOptions, configurableAsanaClient.Options);
            var oAuthDispatcher = new AsanaOAuthDispatcher(oAuthApplication, configurableAsanaClient.Options);

            return configurableAsanaClient.WithDispatcher(oAuthDispatcher);
        }
    }
}
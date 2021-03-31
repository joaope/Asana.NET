﻿namespace Asana.OAuth
{
    public static class ConfigurableAsanaClientExtensions
    {
        public static IAsanaClient WithOAuth(
            this IConfigurableAsanaClient configurableAsanaClient,
            OAuthApplicationOptions oAuthApplicationOptions)
        {
            var oAuthApplication = new OAuthApplication(oAuthApplicationOptions, configurableAsanaClient.Options);
            var oAuthDispatcher = new OAuthDispatcher(oAuthApplication, configurableAsanaClient.Options);

            return configurableAsanaClient.WithDispatcher(oAuthDispatcher);
        }
    }
}
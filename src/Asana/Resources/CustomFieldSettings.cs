using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class CustomFieldSettings : Resource
    {
        internal CustomFieldSettings(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<CustomFieldSetting> GetProjectCustomFieldSettings(string projectGid)
        {
            return new GetItemsCollectionRequest<CustomFieldSetting>(Dispatcher, $"projects/{projectGid}/custom_field_settings");
        }

        public GetItemsCollectionRequest<CustomFieldSetting> GetPortfolioCustomFieldSettings(string portfolioGid)
        {
            return new GetItemsCollectionRequest<CustomFieldSetting>(Dispatcher, $"portfolios/{portfolioGid}/custom_field_settings");
        }
    }
}

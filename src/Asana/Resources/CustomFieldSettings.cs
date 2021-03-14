using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class CustomFieldSettings : Resource
    {
        private readonly uint? _defaultPageSize;

        internal CustomFieldSettings(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemsCollectionRequest<CustomFieldSetting> GetProjectCustomFieldSettings(string projectGid)
        {
            return new GetItemsCollectionRequest<CustomFieldSetting>(Dispatcher, _defaultPageSize, $"projects/{projectGid}/custom_field_settings");
        }

        public GetItemsCollectionRequest<CustomFieldSetting> GetPortfolioCustomFieldSettings(string portfolioGid)
        {
            return new GetItemsCollectionRequest<CustomFieldSetting>(Dispatcher, _defaultPageSize, $"portfolios/{portfolioGid}/custom_field_settings");
        }
    }
}

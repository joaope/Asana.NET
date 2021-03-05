using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class OrganizationExports : Resource
    {
        internal OrganizationExports(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public PostItemRequest<OrganizationExportResponse> Create(object data)
        {
            return new PostItemRequest<OrganizationExportResponse>(Dispatcher, "organization_exports").AddData(data);
        }

        public GetItemRequest<OrganizationExportResponse> Get(string organizationExportGid)
        {
            return new GetItemRequest<OrganizationExportResponse>(Dispatcher,
                $"organization_exports/{organizationExportGid}");
        }
    }
}
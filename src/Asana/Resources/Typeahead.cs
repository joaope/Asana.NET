using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Typeahead : Resource
    {
        public Typeahead(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<AsanaNamedResource> GetForWorkspace(
            string workspaceGid,
            string resourceType,
            string? query,
            int? count)
        {
            return new GetItemsCollectionRequest<AsanaNamedResource>(Dispatcher, $"workspaces/{workspaceGid}/typeahead")
                .AddQueryParameter("resource_type", resourceType)
                .AddQueryParameter("query", query)
                .AddQueryParameter("count", count?.ToString());
        }

        public GetItemsCollectionRequest<AsanaNamedResource> GetForWorkspace(string workspaceGid, string resourceType, string? query) 
            => GetForWorkspace(workspaceGid, resourceType, query, null);

        public GetItemsCollectionRequest<AsanaNamedResource> GetForWorkspace(string workspaceGid, string resourceType)
            => GetForWorkspace(workspaceGid, resourceType, null, null);
    }
}
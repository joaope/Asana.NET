using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Typeahead : Resource
    {
        private readonly uint? _defaultPageSize;

        public Typeahead(Dispatcher dispatcher, uint? defaultPageSize) : base(dispatcher)
        {
            _defaultPageSize = defaultPageSize;
        }

        public GetItemsCollectionRequest<AsanaNamedResource> GetForWorkspace(
            string workspaceGid,
            string resourceType,
            string? query,
            int? count)
        {
            return new GetItemsCollectionRequest<AsanaNamedResource>(Dispatcher, _defaultPageSize, $"workspaces/{workspaceGid}/typeahead")
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
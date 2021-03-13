using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Webhooks : Resource
    {
        public Webhooks(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Webhook> GetByWorkspace(string workspaceGid, string? resourceType)
        {
            return new GetItemsCollectionRequest<Webhook>(Dispatcher, "webhooks")
                .AddQueryParameter("workspace", workspaceGid)
                .AddQueryParameter("resource", resourceType);
        }

        public GetItemsCollectionRequest<Webhook> GetByWorkspace(string workspaceGid) =>
            GetByWorkspace(workspaceGid, null);

        public PostItemRequest<Webhook> Establish(object data)
        {
            return new PostItemRequest<Webhook>(Dispatcher, "webhooks").AddData(data);
        }

        public GetItemRequest<Webhook> Get(string webhookGid)
        {
            return new GetItemRequest<Webhook>(Dispatcher, $"webhooks/{webhookGid}");
        }

        public DeleteRequest<EmptyData> Delete(string webhookGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"webhooks/{webhookGid}");
        }
    }
}
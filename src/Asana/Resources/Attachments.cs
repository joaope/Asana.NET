using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Attachments : Resource
    {
        internal Attachments(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<Attachment> Get(string attachmentGid)
        {
            return new GetItemRequest<Attachment>(Dispatcher, $"attachments/{attachmentGid}");
        }

        public DeleteRequest<Attachment> Delete(string attachmentGid)
        {
            return new DeleteRequest<Attachment>(Dispatcher, $"attachments/{attachmentGid}");
        }

        public GetItemsCollectionRequest<Attachment> GetByTask(string taskId)
        {
            return new GetItemsCollectionRequest<Attachment>(Dispatcher, $"tasks/{taskId}/attachments");
        }

        public PostItemRequest<Attachment> Upload(string taskId, string fileName, byte[] fileData)
        {
            return (PostItemRequest<Attachment>) new PostItemRequest<Attachment>(Dispatcher, $"tasks/{taskId}/attachments")
                .AddFile(fileData, fileName);
        }
    }
}
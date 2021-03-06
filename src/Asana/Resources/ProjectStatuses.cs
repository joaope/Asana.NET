using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class ProjectStatuses : Resource
    {
        public ProjectStatuses(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<ProjectStatus> Get(string projectStatusGid)
        {
            return new GetItemRequest<ProjectStatus>(Dispatcher, $"project_statuses/{projectStatusGid}");
        }

        public DeleteRequest<EmptyData> Delete(string projectStatusGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"project_statuses/{projectStatusGid}");
        }

        public GetItemsCollectionRequest<ProjectStatus> GetFromProject(string projectGid)
        {
            return new GetItemsCollectionRequest<ProjectStatus>(Dispatcher, $"projects/{projectGid}/project_statuses");
        }

        public PostItemRequest<ProjectStatus> CreateOnProject(string projectGid, object data)
        {
            return new PostItemRequest<ProjectStatus>(Dispatcher, $"projects/{projectGid}/project_statuses")
                .AddData(data);
        }
    }
}
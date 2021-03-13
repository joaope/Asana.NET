using System;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class Projects : Resource
    {
        internal Projects(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Project> GetByTeam(string teamGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, "projects").AddQueryParameter("team", teamGid);
        }

        public GetItemsCollectionRequest<Project> GetByWorkspace(string workspaceGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, "projects").AddQueryParameter("workspace", workspaceGid);
        }

        public GetItemsCollectionRequest<Project> GetByOrganization(string organizationGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, "projects").AddQueryParameter("workspace", organizationGid);
        }

        public PostItemRequest<Project> Create(object data)
        {
            return new PostItemRequest<Project>(Dispatcher, "projects").AddData(data);
        }

        public GetItemRequest<Project> Get(string gid)
        {
            if (string.IsNullOrEmpty(gid))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(gid));
            }

            return new GetItemRequest<Project>(Dispatcher, $"projects/{gid}");
        }

        public PutItemRequest<Project> Update(string projectGid, object data)
        {
            return new PutItemRequest<Project>(Dispatcher, $"projects/{projectGid}").AddData(data);
        }

        public DeleteRequest<EmptyData> Delete(string projectGid)
        {
            return new DeleteRequest<EmptyData>(Dispatcher, $"projects/{projectGid}");
        }

        public PostItemRequest<Job> Duplicate(string projectGid, object data)
        {
            return new PostItemRequest<Job>(Dispatcher, $"projects/{projectGid}/duplicate").AddData(data);
        }

        public GetItemsCollectionRequest<Project> GetTaskProjects(string taskGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, $"tasks/{taskGid}/projects");
        }

        public GetItemsCollectionRequest<Project> GetTeamProjects(string teamGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, $"teams/{teamGid}/projects");
        }

        public GetItemsCollectionRequest<Project> GetTeamProjects(string teamGid, bool archived) =>
            GetTeamProjects(teamGid).AddQueryParameter("archived", archived.ToString().ToLowerInvariant());

        public PostItemRequest<Project> CreateTeamProject(string teamGid, object data)
        {
            return new PostItemRequest<Project>(Dispatcher, $"teams/{teamGid}/projects").AddData(data);
        }

        public GetItemsCollectionRequest<Project> GetWorkspaceProjects(string workspaceGid)
        {
            return new GetItemsCollectionRequest<Project>(Dispatcher, $"workspaces/{workspaceGid}/projects");
        }

        public GetItemsCollectionRequest<Project> GetWorkspaceProjects(string workspaceGid, bool archived) =>
            GetWorkspaceProjects(workspaceGid).AddQueryParameter("archived", archived.ToString().ToLowerInvariant());

        public PostItemRequest<Project> CreateWorkspaceProject(string workspaceGid, object data)
        {
            return new PostItemRequest<Project>(Dispatcher, $"workspaces/{workspaceGid}/projects").AddData(data);
        }

        public PostItemRequest<CustomFieldSetting> CreateCustomField(string projectGid, object data)
        {
            return new PostItemRequest<CustomFieldSetting>(Dispatcher, $"projects/{projectGid}/addCustomFieldSetting")
                .AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveCustomField(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/removeCustomFieldSetting")
                .AddData(data);
        }

        public GetItemRequest<TaskCountResponse> GetTaskCounts(string projectGid)
        {
            return new GetItemRequest<TaskCountResponse>(Dispatcher, $"projects/{projectGid}");
        }

        public PostItemRequest<EmptyData> AddUsers(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/addMembers").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveUsers(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/removeMembers").AddData(data);
        }

        public PostItemRequest<EmptyData> AddFollowers(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/addFollowers").AddData(data);
        }

        public PostItemRequest<EmptyData> RemoveFollowers(string projectGid, object data)
        {
            return new PostItemRequest<EmptyData>(Dispatcher, $"projects/{projectGid}/removeFollowers").AddData(data);
        }
    }
}

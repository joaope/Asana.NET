using System;
using Asana.Models;
using Asana.Requests;
using Newtonsoft.Json;

namespace Asana.Resources
{
    public sealed class Tasks : Resource
    {
        internal Tasks(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemsCollectionRequest<Task> GetFiltered(
            string? assigneeGid,
            string? projectGid,
            string? sectionGid,
            string? workspaceGid,
            DateTime? completedSince,
            DateTime? modifiedSince)
        {
            return new GetItemsCollectionRequest<Task>(Dispatcher, $"tasks")
                .AddQueryParameter("assignee", assigneeGid)
                .AddQueryParameter("project", projectGid)
                .AddQueryParameter("section", sectionGid)
                .AddQueryParameter("workspace", workspaceGid)
                .AddQueryParameter("completed_since",
                    completedSince.HasValue ? JsonConvert.SerializeObject(completedSince) : null)
                .AddQueryParameter("modified_since",
                    modifiedSince.HasValue ? JsonConvert.SerializeObject(modifiedSince) : null);
        }

        public GetItemsCollectionRequest<Task> GetFromProject(string projectGid)
        {
            return new GetItemsCollectionRequest<Task>(Dispatcher, $"projects/{projectGid}/tasks");
        }

        public DeleteRequest<Task> Delete(string taskGid)
        {
            return new DeleteRequest<Task>(Dispatcher, $"tasks/{taskGid}");
        }
    }
}
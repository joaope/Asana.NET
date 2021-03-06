using Asana.Dispatchers;
using Asana.Models;
using Asana.Requests;

namespace Asana.Resources
{
    public sealed class UserTaskLists : Resource
    {
        public UserTaskLists(Dispatcher dispatcher) : base(dispatcher)
        {
        }

        public GetItemRequest<UserTaskList> Get(string userTaskListGid)
        {
            return new GetItemRequest<UserTaskList>(Dispatcher, $"user_task_lists/{userTaskListGid}");
        }

        public GetItemRequest<UserTaskList> GetUserTaskList(string userGid, string workspaceGid)
        {
            return new GetItemRequest<UserTaskList>(Dispatcher, $"users/{userGid}/user_task_list")
                .AddQueryParameter("workspace", workspaceGid);
        }

        public GetItemRequest<UserTaskList> GetUserTaskList(string userGid)
        {
            return new GetItemRequest<UserTaskList>(Dispatcher, $"users/{userGid}/user_task_list");
        }
    }
}
using System.Threading.Tasks;

namespace Asana.Console
{
    public static class Program
    {
        public static async Task Main()
        {
            var client = Asana.Client.FromAccessToken("1/1199924348796434:c37a27f0ab8b35cce35b119d0d9e411e");

            //var workspaces = await client.Workspaces.GetAll().Execute();

            //var tags = await client.Tags.GetAll().Execute();

            //var createTag = await client.Tags.CreateWorkspaceTag("1199924348974039", new
            //{
            //    name = "New Prog tag"
            //}).Execute();

            //var createTeam = await client.Teams.Create(new
            //{
            //    description = "All developers should be members of this team.",
            //    html_description = "<body><em>All</em> developers should be members of this team.</body>",
            //    name = "Marketing TEAMMZZZ",
            //    organization = "1199924348974041"
            //}).Execute();

            //var user = await client.Users.GetFavouriteTags("1199924348974087", "1199924348974039").Execute();

            //var attachment = await client.Attachments.GetByTask("1199924348974087").AddField("download_url").Execute();

            //var upload = await client.Attachments.Upload("1199924348974087", "comprovativo.pdf", File.ReadAllBytes("C:\\Users\\JoaoP\\Desktop\\comprovativo.pdf")).AddField("download_url").Execute();

            //var project = client.Tasks.GetFromProject("1199924348974058").ExecuteAsEnumerable(2);

            //await foreach (var p in project)
            //{

            //}

            //var deleteTask = await client.Tasks.Delete("1199965680272782").PrettyOutput(true).Execute();
        }
    }
}

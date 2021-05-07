using Asana.Models;
using Asana.Requests;
using Asana.Tests.Utils;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Asana.Tests.Unit.Requests
{
    public sealed class DeleteRequestTests
    {
        [Fact]
        public async Task Test()
        {
            var dispatcher = new MockDispatcher(AsanaClientOptions.Default);
            var deleteRequest = new DeleteRequest<MockData>(dispatcher, "/delete/path");

            await deleteRequest.Execute();
        }

        private sealed class MockData : IData
        {

        }
    }
}

using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Asana.Tests.Utils
{
    public sealed class MockLoggerFactory : ILoggerFactory
    {
        public List<MockLogger> CreatedLoggers { get; } = new List<MockLogger>();

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            var logger = new MockLogger();
            CreatedLoggers.Add(logger);

            return logger;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            throw new System.NotImplementedException();
        }
    }
}
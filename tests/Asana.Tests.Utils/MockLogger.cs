using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Asana.Tests.Utils
{
    public sealed class MockLogger : ILogger
    {
        public List<(LogLevel LogLevel, EventId EventId, string Message, Exception Exception)> EmittedEvents { get; } =
            new List<(LogLevel, EventId, string, Exception)>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            EmittedEvents.Add((logLevel, eventId, formatter(state, exception), exception));
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
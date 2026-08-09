using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2.Tests;

public class MockLoggerFactory : ILoggerFactory
{
    public ILogger CreateLogger(string categoryName)
    {
        return Mock.Logger(categoryName);
    }

    public void AddProvider(ILoggerProvider provider)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
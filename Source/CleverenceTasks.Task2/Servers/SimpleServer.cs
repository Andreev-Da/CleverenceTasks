using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2;

public class SimpleServer(ILogger<SimpleServer> logger) : IServer
{
    private volatile int _count;
    
    public int GetCount()
    {
        return _count;
    }

    public void AddToCount(int value)
    {
        int count = _count;
        int newValue = value;
        _count = newValue;

        logger.LogDebug("Count changed {0} -> {1}", count, newValue);
    }
}
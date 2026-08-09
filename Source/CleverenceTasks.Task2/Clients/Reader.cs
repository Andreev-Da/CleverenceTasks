using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2;

public class Reader(IServer server, ILogger logger) : IClient
{
    public void DoWork()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var count = server.GetCount();
        stopwatch.Stop();
        logger.LogDebug("Read {0} {1} ms", count, stopwatch.Elapsed.TotalMilliseconds);
    }
}
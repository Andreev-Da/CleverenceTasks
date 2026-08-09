using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2.Clients;

public class Writer(IServer server, ILogger<Writer> logger, int count = 1) : IClient
{
    public void DoWork()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        server.AddToCount(count);
        stopwatch.Stop();
        logger.LogDebug("Writed {0} {1} ms", count,  stopwatch.Elapsed.TotalMilliseconds);
    }
}
using TUnit.Core.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace CleverenceTasks.Task2.Tests;

public class MultithreadingTests
{
    private const int Increment = 1;
    
    private readonly IServer _server;
    private readonly ClientFactory _clientsFactory;
    
    public MultithreadingTests()
    {
        _server = new SimpleServer();
        _clientsFactory = new ClientFactory(_server, new MockLoggerFactory(), Increment);
    }
    
    [Test]
    [Arguments(0, 8)]
    [Arguments(0, 32)]
    [Arguments(0, 64)]
    [Arguments(0, 128)]
    public async Task RaceTest(int readersCount, int writersCount)
    {
        IReadOnlyCollection<IClient> clients = _clientsFactory.CreateMany(readersCount, writersCount);
        List<Thread> threads = new List<Thread>(clients.Count);
        SemaphoreSlim launcher = new SemaphoreSlim(0, clients.Count);
        
        // Не использовал Parallel, т.к. он вроде бы работает с ThreadPool, а хочется честную конкуренцию
        foreach (IClient client in clients)
        {
            var thread = new Thread(() =>
            {
                launcher.Wait();
                Thread.Sleep(1);
                client.DoWork();
            });
            
            thread.Start();
            threads.Add(thread);
        }
        
        launcher.Release(clients.Count);

        foreach (Thread thread in threads)
        {
            thread.Join();
        }
        
        await Assert.That(_server.GetCount()).IsEqualTo(writersCount * Increment);
    }
}
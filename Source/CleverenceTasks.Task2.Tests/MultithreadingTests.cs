using CleverenceTasks.Task2.Servers;
using Microsoft.Extensions.Logging;
using TUnit.Core.Executors;
using TUnit.Core.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace CleverenceTasks.Task2.Tests;

/// <summary>
/// Тесты поведения в многопоточной среде. Словно лучше подошли бы какие то стрес тесты, либо что то более похожее на фаззинг. 
/// Т.к. данные тесты не идемпотентны и могут давать разные результаты
/// </summary>
public class MultithreadingTests
{
    private const int Increment = 1;
    
    /// <summary>
    /// Тест 50 на 50, авось повезет и мы встретим гонку потоков
    /// </summary>
    [Test]
    [CombinedDataSources]
    [NotInParallel]
    public async Task RaceConditionTest(
        [ServersDataSource] IServer server,
        [Arguments(8, 16, 32, 64, 128)] int writersCount
    ){
        IReadOnlyCollection<IClient> clients = new ClientFactory(server, new MockLoggerFactory(), Increment)
            .CreateMany(readersCount: 0, writersCount);

        Run(clients);
        
        await Assert.That(server.GetCount()).IsEqualTo(writersCount * Increment);
    }

    /// <summary>
    /// Обычный прогон, гипотетически можем выявить взаимоблокировки
    /// </summary>
    [Test]
    [CombinedDataSources]
    [NotInParallel]
    public async Task RegularTest(
        [ServersDataSource] IServer server,
        [Arguments(8, 16, 32)] int readersCount,
        [Arguments(8, 16, 32)] int writersCount
    )
    {
        IReadOnlyCollection<IClient> clients = new ClientFactory(server, new MockLoggerFactory(), Increment)
            .CreateMany(readersCount: readersCount, writersCount);

        Run(clients);

        await Assert.That(server.GetCount()).IsEqualTo(writersCount * Increment);
    }


    private void Run(IReadOnlyCollection<IClient> clients)
    {
        List<Thread> threads = new List<Thread>(clients.Count);
        SemaphoreSlim launcher = new SemaphoreSlim(0, clients.Count);

        // Не использовал Parallel, т.к. он вроде бы работает с ThreadPool, а хочется честную конкуренцию
        foreach (IClient client in clients)
        {
            var thread = new Thread(() =>
            {
                launcher.Wait();
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
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
    }
}
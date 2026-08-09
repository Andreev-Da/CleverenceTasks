using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Dataflow;
using CleverenceTasks.Task2.Clients;
using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2;

public class ClientFactory(IServer server, ILoggerFactory logger, int increment = 1)
{
    public IClient CreateNew(bool isWriter = false)
    {
        return isWriter ? new Writer(server, logger.CreateLogger<Writer>(), increment) 
                : new Reader(server, logger.CreateLogger<Reader>());
    }   

    public IReadOnlyCollection<IClient> CreateMany(int readersCount = 0, int writersCount = 0)
    {
        List<IClient> clients = new List<IClient>(readersCount + writersCount);

        for (int _ = 0; _ < readersCount; _++)
        {
            clients.Add(CreateNew(isWriter: false));
        }
        
        for (int _ = 0; _ < writersCount; _++)
        {
            clients.Add(CreateNew(isWriter: true));
        }

        var span = CollectionsMarshal.AsSpan(clients);
        Random.Shared.Shuffle(span);
        
        return clients;
    }
}
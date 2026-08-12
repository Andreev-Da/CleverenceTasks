using Microsoft.Extensions.Logging;

namespace CleverenceTasks.Task2.Servers
{
    public class ReaderWriterServer(ILogger<ReaderWriterServer> logger) : IServer
    {
        private Utils.ReaderWriterLock _lock = new();
        private int _count;

        public int GetCount()
        {
            return _count;
        }

        public void AddToCount(int value)
        {
            using var scope = _lock.WriteScope();

            int count = _count;
            int newValue = count + value;
            _count = newValue;

            logger.LogDebug("Count changed {0} -> {1}", count, newValue);
        }
    }
}

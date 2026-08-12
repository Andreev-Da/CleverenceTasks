using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests.Mocks
{
    internal class CollectionLogReader(IReadOnlyCollection<Log> logs) : ILogReader
    {
        private int _lastIndex = -1;

        public async Task<Log?> ReadNextAsync(CancellationToken cancellation = default)
        {
            _lastIndex++;
            if (_lastIndex >= logs.Count)
                return null;

            // В конце концов я ожидаю асинхронное взаимодействие, поэтому его и симулирую
            await Task.Yield();
            return logs.ElementAt(_lastIndex);
        }
    }
}

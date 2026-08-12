using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests.Mocks
{
    internal class CollectionLogWriter(IList<Log> output) : ILogWriter
    {
        public async Task WriteAsync(Log log, CancellationToken cancellation)
        {
            // В конце концов я ожидаю асинхронное взаимодействие, поэтому его и симулирую
            await Task.Yield();

            output.Add(log);
        }

        public void Dispose()
        {
        }
    }
}

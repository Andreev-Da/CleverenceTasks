using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    internal class Translator(ILogReader reader, ILogWriter writer)
    {
        public async Task TranslateAsync(CancellationToken cancellation)
        {
            LogData? log = await reader.ReadNextAsync(cancellation);

            while (log != null)
            {
                cancellation.ThrowIfCancellationRequested();

                Task writeTask = writer.WriteAsync(log, cancellation);
                log = await reader.ReadNextAsync(cancellation);

                await writeTask;
            }
        }
    }
}

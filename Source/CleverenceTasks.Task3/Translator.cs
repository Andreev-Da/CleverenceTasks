using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public class Translator(ILogReader reader, ILogWriter writer)
    {
        public async Task TranslateAsync(CancellationToken cancellation = default)
        {
            Log? log = await reader.ReadNextAsync(cancellation);

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

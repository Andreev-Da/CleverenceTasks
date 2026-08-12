using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public class Translator : Disposable
    {
        private readonly ILogReader _reader;
        private readonly ILogWriter _writer;

        public Translator(ILogReader reader, ILogWriter writer)
        {
            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(writer);

            _reader = reader;
            _writer = writer;
        }

        public async Task TranslateAsync(CancellationToken cancellation = default)
        {
            Log? log = await _reader.ReadNextAsync(cancellation);

            while (log != null)
            {
                cancellation.ThrowIfCancellationRequested();

                Task writeTask = _writer.WriteAsync(log, cancellation);
                log = await _reader.ReadNextAsync(cancellation);

                await writeTask;
            }
        }
    }
}

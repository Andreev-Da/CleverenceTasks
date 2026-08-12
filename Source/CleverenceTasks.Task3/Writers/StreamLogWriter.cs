using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Writers
{
    public class StreamLogWriter : Disposable, ILogWriter
    {
        private Func<Log, string> _formatter;
        private StreamWriter _output;

        public StreamLogWriter(Stream output, Func<Log, string> formatter)
            : this(new StreamWriter(output), formatter)
        {
        }

        public StreamLogWriter(StreamWriter output, Func<Log, string> formatter)
        {
            ArgumentNullException.ThrowIfNull(output);
            ArgumentNullException.ThrowIfNull(formatter);

            _formatter = formatter;
            _output = output;
        }


        public async Task WriteAsync(Log log, CancellationToken cancellation = default)
        {
            string formattedLog = _formatter(log);
            await _output.WriteLineAsync(formattedLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Я параноик, вроде бы writer'ы могли стримы освобождать
                // а это немного нежелательное поведение
                _output.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}

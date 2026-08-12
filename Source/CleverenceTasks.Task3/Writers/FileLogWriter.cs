using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Writers
{
    internal class FileLogWriter : ILogWriter
    {
        private Func<LogData, string> _formatter;

        public FileLogWriter(StreamWriter stream, Func<LogData, string> formatter)
        {
            ArgumentNullException.ThrowIfNull(formatter);
            _formatter = formatter;
        }

        public Task WriteAsync(LogData log, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}

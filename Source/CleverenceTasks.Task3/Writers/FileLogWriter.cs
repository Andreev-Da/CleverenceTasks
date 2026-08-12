using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Writers
{
    internal class FileLogWriter : ILogWriter
    {
        private Func<Log, string> _formatter;

        public FileLogWriter(StreamWriter stream, Func<Log, string> formatter)
        {
            ArgumentNullException.ThrowIfNull(formatter);
            _formatter = formatter;
        }

        public Task WriteAsync(Log log, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public interface ILogWriter
    {
        Task WriteAsync(LogData log, CancellationToken cancellation);
    }
}

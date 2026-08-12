using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    internal interface ILogReader
    {
        /// <summary>
        /// Read next log if it exists
        /// </summary>
        /// <returns>While there are unread logs, LogData returns otherwise Null</returns>
        Task<LogData?> ReadNextAsync(CancellationToken cancellation = default);
    }
}

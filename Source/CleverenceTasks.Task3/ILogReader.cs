using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public interface ILogReader : IDisposable
    {
        /// <summary>
        /// Read next log if it exists
        /// </summary>
        /// <returns>While there are unread logs, LogData returns otherwise Null</returns>
        Task<Log?> ReadNextAsync(CancellationToken cancellation = default);
    }
}

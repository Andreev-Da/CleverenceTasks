using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public record class Log(
        DateTimeOffset DateTime,
        LogLevel LogLevel,
        string? CallerMemberName,
        string Message
    );
}

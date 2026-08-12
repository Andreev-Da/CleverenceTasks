using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public interface ILogParser
    {
        Log Parse(ReadOnlySpan<char> line);
    }
}

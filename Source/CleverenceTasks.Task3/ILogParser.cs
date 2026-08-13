using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public interface ILogParser
    {
        // Я только потом понял, что такое API лешило меня возможности использовать регулярки
        Log Parse(ReadOnlySpan<char> line);
    }
}

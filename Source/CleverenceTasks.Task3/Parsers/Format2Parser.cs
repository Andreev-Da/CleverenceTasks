using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Parsers
{
    //Формат 2: 2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'
    public class Format2Parser : ILogParser
    {
        public Log Parse(ReadOnlySpan<char> line)
        {
            throw new NotImplementedException();
        }
    }
}

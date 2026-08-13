using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3
{
    public class ParseException(
        string? message = null, 
        Exception? innerException = null
    ) : Exception(message, innerException)
    {
    }
}

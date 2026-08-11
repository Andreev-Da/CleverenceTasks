using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task2.Utils
{
    internal readonly struct Defer(Action action) : IDisposable
    {
        public void Dispose()
        {
            action();
        }
    }
}

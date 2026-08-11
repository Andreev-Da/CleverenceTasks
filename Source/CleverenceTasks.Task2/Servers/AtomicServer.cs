using CleverenceTasks.Task2.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task2.Servers
{
    public class AtomicServer(ILogger<AtomicServer> logger) : IServer
    {
        private int _count;
        private Utils.ReaderWriterLock _lock = new ();

        public int GetCount()
        {
            using var scope = _lock.ReadScope();
            return _count;
        }

        public void AddToCount(int value)
        {
            using var scope = _lock.WriteScope();

            int count = _count;
            int newValue = count + value;
            _count = newValue;

            logger.LogDebug("Count changed {0} -> {1}", count, newValue);
        }
    }
}

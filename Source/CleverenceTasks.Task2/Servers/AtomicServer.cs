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
        
        public int GetCount()
        {
            return _count;
        }

        public void AddToCount(int value)
        {
            int attempts = 0;
            int count, newValue;

            // Можно просто использовать Interlocked.Add, но я хочу залогировать изменение значения
            do
            {
                attempts++;
                count = _count;
                newValue = _count + value;
            } while (Interlocked.CompareExchange(ref _count, newValue, count) != count);

            logger.LogDebug("Count changed {0} -> {1} ({2})", count, newValue, attempts);
        }
    }
}

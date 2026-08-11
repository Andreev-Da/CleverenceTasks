// Можно было бы использовать дефолтный ReaderWriterLock, но мы же тут чтобы понтануться
// Адаптировал реализацию из статьи https://habr.com/ru/articles/200924/ под условия задачи
// По факту адаптировать нечего, алгоритм полностью удовлетворяет условиям

using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task2.Utils
{
    internal class ReaderWriterLock
    {
        /// <summary>
        /// 2 безнаковый бит оставляем под признак активного писателя
        /// 010000000_00000000_00000000_00000000
        /// </summary>
        const int WriterBit = 1 << (sizeof(int) * 8 - 2);

        private volatile int _writersReaders = 0;

        public void ReadLock()
        {
            if (Interlocked.Add(ref _writersReaders, 1) >= WriterBit)
            {
                while (_writersReaders >= WriterBit)
                {
                    Thread.Yield();
                }
            }
        }

        public void ReadUnlock()
        {
            Interlocked.Add(ref _writersReaders, -1);
        }
        
        public void WriteLock()
        {
            while (Interlocked.CompareExchange(ref _writersReaders, WriterBit, 0) != 0)
            {
                Thread.Yield();
            }
        }

        public void WriteUnlock()
        {
            Interlocked.Add(ref _writersReaders, -WriterBit);
        }
        public Defer ReadScope()
        {
            ReadLock();
            return new Defer(ReadUnlock);
        }

        public Defer WriteScope()
        {
            WriteLock();
            return new Defer(WriteUnlock);
        }
    }
}

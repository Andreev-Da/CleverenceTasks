using CleverenceTasks.Task3.Parsers;
using CleverenceTasks.Task3.Readers;
using CleverenceTasks.Task3.Tests.Mocks;
using CleverenceTasks.Task3.Writers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests
{
    internal class ReaderTests
    {
        [Test]
        [LogDataGenerator]
        public async Task Parser1(IReadOnlyCollection<Log> logs)
        {
            var parser = new Format1Parser();

            var result = await RunReader(parser, ToFormat1(logs)).ToListAsync();

            await Assert.That(logs.Count).IsEqualTo(result.Count);
        }

        private async IAsyncEnumerable<Log> RunReader(Format1Parser parser, string value)
        {
            using var reader = new StreamLogReader(new StringReader(value), parser);

            Log? log = await reader.ReadNextAsync();
            while (log != null)
            {
                yield return log;
                log = await reader.ReadNextAsync();
            }
        }

        private string ToFormat1(IReadOnlyCollection<Log> logs)
        {
            return string.Join("\n", logs.Select(LogFormatter1));
        }


        private static string LogFormatter1(Log log)
        {
            //Формат 1: 10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'
            //Дата: 10.03.2025
            //Время: 15:14:49.523
            //УровеньЛогирования: INFORMATION
            //Сообщение: Версия программы: ‘3.4.0.48729’
            var builder = new StringBuilder();

            builder.AppendFormat("{0:dd.MM.yyyy} ", log.DateTime);
            builder.AppendFormat("{0:H:mm:ss.FFF} ", log.DateTime);
            builder.AppendFormat("{0} ", GetLogLevelString(log.LogLevel));
            builder.AppendFormat("{0}", log.Message);

            return builder.ToString();
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Debug => "DEBUG",
                LogLevel.Warning => "WARNING",
                LogLevel.Information => "INFORMATION",
                LogLevel.Error => "ERROR"
            };
        }
    }
}

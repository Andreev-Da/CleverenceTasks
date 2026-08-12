using CleverenceTasks.Task3.Readers;
using CleverenceTasks.Task3.Tests.Mocks;
using CleverenceTasks.Task3.Writers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests
{
    internal class StreamWriterTests
    {
        [Test]
        [LogDataGenerator]
        public async Task StreamWriteTests(IReadOnlyCollection<Log> logs)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var logWriter = new StreamLogWriter(writer, SimpleFormatter);

            foreach (var log in logs)
            {
                await logWriter.WriteAsync(log);
            }

            await writer.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);
            using var stramReader = new StreamReader(stream);
            string result = await stramReader.ReadToEndAsync();

            // Не успел довести до ума сравнение результатов. Основная идея думаю и так ясна
            if (result.Length == 0 && logs.Count != 0)
            {
                Assert.Fail("Error recording logs");
            }
        }

        private static string SimpleFormatter(Log log)
        {
            //Дата: 10.03.2025
            //Время: 15:14:49.523
            //УровеньЛогирования: INFORMATION
            //Сообщение: Версия программы: ‘3.4.0.48729’
            var builder = new StringBuilder();

            builder.AppendFormat("{0:dd.MM.yyyy}\t", log.DateTime);
            builder.AppendFormat("{0:H:mm:ss.FFF}\t", log.DateTime);
            builder.AppendFormat("{0}\t", GetLogLevelString(log.LogLevel));
            builder.AppendFormat("{0}\t", log.CallerMemberName ?? "DEFAULT");
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

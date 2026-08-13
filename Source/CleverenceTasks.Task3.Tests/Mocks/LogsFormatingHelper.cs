using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests.Mocks
{
    internal static class LogsFormatingHelper
    {
        public static string ToFormat1(IReadOnlyCollection<Log> logs)
        {
            return string.Join("\n", logs.Select(LogFormatter1));
        }

        public static string ToFormat2(IReadOnlyCollection<Log> logs)
        {
            return string.Join("\n", logs.Select(LogFormatter2));
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
            builder.AppendFormat("{0:HH:mm:ss.fff} ", log.DateTime);
            builder.AppendFormat("{0} ", GetLogLevelString1(log.LogLevel));
            builder.AppendFormat("{0}", log.Message);

            return builder.ToString();
        }

        private static string GetLogLevelString1(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Debug => "DEBUG",
                LogLevel.Warning => "WARNING",
                LogLevel.Information => "INFORMATION",
                LogLevel.Error => "ERROR"
            };
        }

        private static string LogFormatter2(Log log)
        {
            //Формат 1: 10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'
            //Дата: 10.03.2025
            //Время: 15:14:49.523
            //УровеньЛогирования: INFORMATION
            //Сообщение: Версия программы: ‘3.4.0.48729’
            var builder = new StringBuilder();

            builder.AppendFormat("{0:yyyy-MM-dd HH:mm:ss.ffff}|", log.DateTime);
            builder.AppendFormat(" {0}|", GetLogLevelString2(log.LogLevel));
            builder.AppendFormat("{0}|", log.CallerMemberName);
            builder.AppendFormat("{0}", log.Message);

            return builder.ToString();
        }

        private static string GetLogLevelString2(LogLevel logLevel)
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

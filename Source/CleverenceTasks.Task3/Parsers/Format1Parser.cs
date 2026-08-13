using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CleverenceTasks.Task3.Parsers
{
    /// <summary>
    /// Формат 1: 10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'
    /// </summary>
    public class Format1Parser : ILogParser
    {
        public Log Parse(ReadOnlySpan<char> line)
        {
            int index = 0;
            // ЗНАЮ что надо это в кастомный StringReader или токенизатор строки обернуть, но времени нет, тут увы.
            // Я считаю что логи в целом довольно хорошо структурированы, так что реализация должна быть удовлетворительная.

            var dateTime = ReadDateTime(line, ref index);
            var logLevel = ReadLogLevel(line, ref index);
            var message = ReadMessage(line, ref index);

            return new Log(dateTime, logLevel, null, message);
        }

        private string ReadMessage(ReadOnlySpan<char> line, ref int index)
        {
            while (index < line.Length && Char.IsWhiteSpace(line[index]))
            {
                index++;
            }

            if (index >= line.Length)
                return string.Empty;

            return line.Slice(index, line.Length - index).ToString();
        }

        private LogLevel ReadLogLevel(ReadOnlySpan<char> line, ref int index)
        {
            var start = index;

            while (index < line.Length && !Char.IsWhiteSpace(line[index]))
                index++;

            var levelString = line.Slice(start, index - start);

            return levelString switch
            {
                "INFORMATION" => LogLevel.Information,
                "DEBUG" => LogLevel.Debug,
                "ERROR" => LogLevel.Error,
                "WARNING" => LogLevel.Warning,
                _ => throw new ParseException($"Invalid token {levelString}")
            };
        }

        private DateTimeOffset ReadDateTime(ReadOnlySpan<char> line, ref int index)
        {
            var start = index;

            while (index < line.Length && !Char.IsLetter(line[index]))
                index++;

            var dateTimeString = line.Slice(start, index - start - 1);
            var isSuccess = DateTimeOffset.TryParseExact(
                dateTimeString,
                "dd.MM.yyyy HH:mm:ss.fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal, 
                out var result);
        
            if (!isSuccess)
                throw new ParseException($"Expected datetime {dateTimeString}");

            return result;
        }
    }
}

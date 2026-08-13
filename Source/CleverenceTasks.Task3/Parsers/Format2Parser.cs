using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Text;

namespace CleverenceTasks.Task3.Parsers
{
    //Формат 2: 2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'
    public class Format2Parser : ILogParser
    {
        public Log Parse(ReadOnlySpan<char> line)
        {
            int index = 0;
            var dateTime = ReadDateTime(line, ref index);
            var logLevel = ReadLogLevel(line, ref index);
            var callerName = ReadCallerName(line, ref index);
            var message = ReadMessage(line, ref index);

            return new Log(dateTime, logLevel, callerName, message);
        }

        private string? ReadCallerName(ReadOnlySpan<char> line, ref int index)
        {
            if (line[index] == '|' && line[index + 1] == '|')
                return null;

            SkipWhiteSpaceAndSeparators(line, ref index);
            var start = index;
            ReadBeforeSeparator(line, ref index);

            return line.Slice(start, index - start - 1).ToString();
        }

        private string ReadMessage(ReadOnlySpan<char> line, ref int index)
        {
            SkipWhiteSpaceAndSeparators(line, ref index);

            if (index >= line.Length)
                return string.Empty;

            return line.Slice(index).ToString();
        }

        private LogLevel ReadLogLevel(ReadOnlySpan<char> line, ref int index)
        {
            SkipWhiteSpaceAndSeparators(line, ref index);
            var start = index;
            ReadBeforeSeparator(line, ref index);

            var levelString = line.Slice(start, index - start);

            if (levelString.StartsWith("INFO"))
                return LogLevel.Information;

            if (levelString.StartsWith("DEBUG"))
                return LogLevel.Debug;

            if (levelString.StartsWith("ERROR"))
                return LogLevel.Error;

            if (levelString.StartsWith("WARN"))
                return LogLevel.Warning;

            throw new ParseException($"Invalid token {levelString}");
        }

        private DateTimeOffset ReadDateTime(ReadOnlySpan<char> line, ref int index)
        {
            SkipWhiteSpaceAndSeparators(line, ref index);
            var start = index;
            ReadBeforeSeparator(line, ref index);

            var dateTimeString = line.Slice(start, index - start);
            var isSuccess = DateTimeOffset.TryParseExact(
                dateTimeString,
                "yyyy-MM-dd HH:mm:ss.ffff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out var result);

            if (!isSuccess)
                throw new ParseException($"Expected datetime {dateTimeString}");

            return result;
        }

        private void SkipWhiteSpaceAndSeparators(ReadOnlySpan<char> line, ref int index)
        {
            char symbol = line[index];
            while (Char.IsWhiteSpace(symbol) || symbol == '|')
            {
                index++;
                symbol = line[index];
            }
        }

        private void ReadBeforeSeparator(ReadOnlySpan<char> line, ref int index)
        {
            char symbol = line[index];
            while (symbol != '|')
            {
                index++;

                if (index >= line.Length)
                    throw new ParseException("Expected separator, incorrect log format");

                symbol = line[index];

            }
        }
    }
}

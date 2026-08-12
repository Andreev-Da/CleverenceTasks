using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task3.Tests.Mocks
{
    internal class LogDataGeneratorAttribute : DataSourceGeneratorAttribute<IReadOnlyCollection<Log>>
    {
        private static readonly IReadOnlyCollection<Log> _testData;
        private static readonly string?[] _callerNames;

        static LogDataGeneratorAttribute()
        {
            _callerNames = [null, "Class1312.Method2", "SimpleClass.SomeMethod"];
            _testData = CreateTestData();
        }

        protected override IEnumerable<Func<IReadOnlyCollection<Log>>> GenerateDataSources(DataGeneratorMetadata dataGeneratorMetadata)
        {
            yield return () => _testData;
            yield return () => [];
        }

        public static IReadOnlyCollection<Log> CreateTestData()
        {
            DateTimeOffset start = new DateTimeOffset(2026, 08, 12, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset end = new DateTimeOffset(2026, 08, 15, 0, 0, 0, TimeSpan.Zero);
            int logsCount = 250;
            var logLevelsCount = Enum.GetValues(typeof(LogLevel)).Length;

            TimeSpan period = (end - start) / logsCount;
            
            var logs = new List<Log>();
            
            for (int i = 0; i < logsCount; i++)
            {
                Log log = new(
                    start + i * period,
                    (LogLevel)(i % logLevelsCount),
                    GetCallerMemberName(i),
                    GetMessage(i)
                );

                logs.Add(log);
            }

            return logs.AsReadOnly();
        }

        private static string GetMessage(int i)
        {
            return $"Message _ номер {i}";
        }

        private static string? GetCallerMemberName(int i)
        {
            return _callerNames[i % _callerNames.Length];
        }
    }
}

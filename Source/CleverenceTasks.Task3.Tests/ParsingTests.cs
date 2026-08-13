using CleverenceTasks.Task3.Parsers;
using CleverenceTasks.Task3.Readers;
using CleverenceTasks.Task3.Tests.Mocks;
using CleverenceTasks.Task3.Writers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CleverenceTasks.Task3.Tests
{
    internal class ParsingTests
    {
        [Test]
        [LogDataGenerator]
        public async Task Formater1Test(IReadOnlyCollection<Log> logs)
        {
            var parser = new Format1Parser();

            var result = await RunReader(parser, LogsFormatingHelper.ToFormat1(logs)).ToListAsync();

            await Assert.That(logs.Count).IsEqualTo(result.Count);
        }

        [Test]
        [LogDataGenerator]
        public async Task Formater2Test(IReadOnlyCollection<Log> logs)
        {
            var parser = new Format2Parser();

            var result = await RunReader(parser, LogsFormatingHelper.ToFormat2(logs)).ToListAsync();

            await Assert.That(logs.Count).IsEqualTo(result.Count);
        }


        [Test]
        [LogDataGenerator]
        public async Task Formater1InvalidDataTest(IReadOnlyCollection<Log> logs)
        {
            if (logs.Count == 0)
                return;

            TestInvalidFormatAsync(
                new Format1Parser(),
                LogsFormatingHelper.ToFormat2(logs)
            );
        }

        [Test]
        [LogDataGenerator]
        public async Task Formater2InvalidDataTest(IReadOnlyCollection<Log> logs)
        {
            if (logs.Count == 0)
                return;

            TestInvalidFormatAsync(
                new Format2Parser(), 
                LogsFormatingHelper.ToFormat1(logs)
            );
        }


        private async IAsyncEnumerable<Log> RunReader(ILogParser parser, string value)
        {
            using var reader = new StreamLogReader(new StringReader(value), parser);

            Log? log = await reader.ReadNextAsync();
            while (log != null)
            {
                yield return log;
                log = await reader.ReadNextAsync();
            }
        }

        private async Task TestInvalidFormatAsync(ILogParser parser, string input)
        {
            try
            {
                var result = await RunReader(parser, input).ToListAsync();
            }
            catch (ParseException ex)
            {
                return;
            }

            Assert.Fail("Format parser accepts incorrect input data");
        }
    }
}

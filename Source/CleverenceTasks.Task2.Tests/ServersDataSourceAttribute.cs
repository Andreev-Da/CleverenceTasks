using CleverenceTasks.Task2.Servers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleverenceTasks.Task2.Tests
{
    internal class ServersDataSourceAttribute() : DataSourceGeneratorAttribute<IServer>
    {
        protected override IEnumerable<Func<IServer>> GenerateDataSources(DataGeneratorMetadata dataGeneratorMetadata)
        {
            MockLoggerFactory loggerFactory = new();

            yield return () => new SimpleServer(loggerFactory.CreateLogger<SimpleServer>());
            yield return () => new ReaderWriterServer(loggerFactory.CreateLogger<ReaderWriterServer>());
            yield return () => new AtomicServer(loggerFactory.CreateLogger<AtomicServer>());
        }
    }
}

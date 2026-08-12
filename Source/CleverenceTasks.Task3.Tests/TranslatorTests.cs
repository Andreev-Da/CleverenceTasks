using CleverenceTasks.Task3.Tests.Mocks;

namespace CleverenceTasks.Task3.Tests;

public class TranslatorTests
{
    [Test]
    [LogDataGenerator]
    public async Task ReadWriteTest(IReadOnlyCollection<Log> logs)
    {
        List<Log> output = [];
        using var writer = new CollectionLogWriter(output);
        using var reader = new CollectionLogReader(logs);
        
        using var translator = new Translator(reader, writer);
        
        await translator.TranslateAsync();

        await Assert.That(output).IsEquivalentTo(logs);
    }
}
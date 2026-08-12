using CleverenceTasks.Task3.Tests.Mocks;

namespace CleverenceTasks.Task3.Tests;

public class TranslatorTests
{
    private const int Increment = 1;

    [Test]
    [LogDataGenerator]
    public async Task ReadWriteTest(IReadOnlyCollection<Log> logs)
    {
        List<Log> output = [];
        var writer = new CollectionLogWriter(output);
        var reader = new CollectionLogReader(logs);

        var translator = new Translator(reader, writer);
        await translator.TranslateAsync();

        await Assert.That(output).IsEquivalentTo(logs);
    }
}
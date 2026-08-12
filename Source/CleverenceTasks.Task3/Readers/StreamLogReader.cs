namespace CleverenceTasks.Task3.Readers;

public class StreamLogReader : Disposable, ILogReader
{
    private TextReader _input;
    private ILogParser _parser;

    public StreamLogReader(Stream stream, ILogParser parser)
        : this(new StreamReader(stream), parser)
    {
    }

    public StreamLogReader(TextReader input, ILogParser parser)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(parser);

        _input = input;
        _parser = parser;
    }

    public async Task<Log?> ReadNextAsync(CancellationToken cancellation = default)
    {
        string? line = await _input.ReadLineAsync(cancellation);

        if (line is null)
            return null;

        return _parser.Parse(line);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Я параноик, вроде бы reader'ы могли стримы освобождать
            // а это немного нежелательное поведение
            _input.Dispose();
        }

        base.Dispose(disposing);
    }
}
using System.Dynamic;

namespace CleverenceTasks.Task1;

public static class CharsCompressorExtension
{
    public static IEnumerable<char> Compress(this IEnumerable<char> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        
        using IEnumerator<char> enumerator = source.GetEnumerator();
        
        if (!enumerator.MoveNext())
            yield break;
        
        char lastSymbol = enumerator.Current;
        int count = 0;

        do
        {
            var symbol = enumerator.Current;

            if (symbol == lastSymbol)
            {
                count++;
                continue;
            }
            
            yield return lastSymbol;
            
            if (count > 1)
            {
                foreach (char digit in count.ToString())
                    yield return digit;
            }
            
            lastSymbol = symbol;
            count = 1;
        } while (enumerator.MoveNext());
        
        yield return lastSymbol;
            
        if (count > 1)
        {
            foreach (char digit in count.ToString())
                yield return digit;
        }
    }
}
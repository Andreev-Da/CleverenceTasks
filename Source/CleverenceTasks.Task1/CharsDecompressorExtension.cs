namespace CleverenceTasks.Task1;

public static class CharsDecompressorExtension
{
    public static IEnumerable<char> Decompress(this IEnumerable<char> source)
    {
        using IEnumerator<char> enumerator = source.GetEnumerator();
        
        if (!enumerator.MoveNext())
            yield break;

        int count = 0;
        bool hasMore = false;
        
        do 
        {
            var symbol = enumerator.Current;
            hasMore = GetRepeatCount(enumerator, out count);
            
            for (int i = 0; i < count; i++)
                yield return symbol;
            
        } while (hasMore);
    }

    private static bool GetRepeatCount(IEnumerator<char> enumerator, out int count)
    {
        const int defaultRepeatCount = 1;
        
        bool hasMore = enumerator.ReadNumber(out count);
        
        if (count == 0)
            count = defaultRepeatCount;
        
        return hasMore;
    }

    private static bool ReadNumber(this IEnumerator<char> enumerator, out int number)
    {
        const int numberSystem = 10;

        number = 0;
        
        while (enumerator.MoveNext())
        {
            int digit = enumerator.Current - '0';

            if (digit < 0 || digit > (numberSystem - 1))
                    return true;

            number = number * numberSystem + digit;
        }

        return false;
    }
}
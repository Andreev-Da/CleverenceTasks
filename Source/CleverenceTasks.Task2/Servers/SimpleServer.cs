namespace CleverenceTasks.Task2;

public class SimpleServer : IServer
{
    private int _count;
    
    public int GetCount()
    {
        return _count;
    }

    public void AddToCount(int value)
    {
        _count += value;
    }
}
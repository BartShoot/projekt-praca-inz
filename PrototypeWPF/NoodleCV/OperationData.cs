namespace NoodleCV;

public class OperationData
{
    protected object _data;
    public Type Type { get; protected set; }

    public T Get<T>()
    {
        if (typeof(T) != Type)
            throw new Exception();

        return (T)_data;
    }
        
    public void Set<T>(T data)
    {
        if (typeof(T) != Type)
            throw new Exception();
        _data = data;
    }
}
namespace NoodleCV;

public class OperationOutput : OperationData
{
    public OperationOutput()
    {
    }

    private OperationOutput(Type type) : this(null, type)
    {
    }
    private OperationOutput(object data, Type type)
    {
        _data = data;
        Type = type;
    }

    public static OperationOutput Create<T>()
    {
        return new OperationOutput(typeof(T));
    }

    public static OperationOutput Create<T>(T data)
    {
        return new OperationOutput(data, typeof(T));
    }
}
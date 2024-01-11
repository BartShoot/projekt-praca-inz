namespace NoodleCV;

public class OperationInput : OperationData
{
    public OperationInput()
    {
    }

    private OperationInput(Type type) : this(null, type)
    {
    }
    private OperationInput(object data, Type type)
    {
        _data = data;
        Type = type;
    }

    public static OperationInput Create<T>()
    {
        return new OperationInput(typeof(T));
    }

    public static OperationInput Create<T>(T data)
    {
        return new OperationInput(data, typeof(T));
    }
}

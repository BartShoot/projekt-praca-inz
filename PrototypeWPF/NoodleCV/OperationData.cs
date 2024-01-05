using System.ComponentModel;

namespace NoodleCV;

public class OperationData : INotifyPropertyChanged
{
    protected object _data;
    public Type Type { get; protected set; }

    public event PropertyChangedEventHandler? PropertyChanged;

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
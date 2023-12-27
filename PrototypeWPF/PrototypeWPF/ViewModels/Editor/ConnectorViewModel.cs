using NoodleCV;
using System.ComponentModel;
using System.Windows;

namespace PrototypeWPF.ViewModels.Editor;

public class ConnectorViewModel : INotifyPropertyChanged
{
    private Point _anchor;

    public ConnectorViewModel()
    {
    }

    public ConnectorViewModel(OperationData data)
    {
        _data = data;
    }

    public Point Anchor
    {
        set
        {
            _anchor = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Anchor)));
        }
        get => _anchor;
    }

    private bool _isConnected;

    public bool IsConnected
    {
        get { return _isConnected; }
        set
        {
            _isConnected = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsConnected)));
        }
    }

    public string Title { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private OperationData _data;

    public OperationData Data
    {
        get { return _data; }
        set
        {
            _data = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Data)));
        }
    }

}

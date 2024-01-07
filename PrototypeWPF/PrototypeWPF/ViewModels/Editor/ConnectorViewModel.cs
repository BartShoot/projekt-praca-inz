using NoodleCV;
using System.Windows;

namespace PrototypeWPF.ViewModels.Editor;

public class ConnectorViewModel : ViewModelBase
{
    private Point _anchor;

    public ConnectorViewModel()
    {
    }

    public ConnectorViewModel(OperationData data)
    {
        Data = data;
    }

    public Point Anchor
    {
        get => _anchor;
        set => SetProperty(ref _anchor, value);
    }

    private bool _isConnected;

    public bool IsConnected
    {
        get => _isConnected;
        set => SetProperty(ref _isConnected, value);
    }

    public string Title { get; set; }

    private OperationData _data;

    public OperationData Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }
}

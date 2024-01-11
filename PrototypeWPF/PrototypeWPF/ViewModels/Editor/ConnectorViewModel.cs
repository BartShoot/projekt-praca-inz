using NoodleCV;
using System;
using System.Windows;

namespace PrototypeWPF.ViewModels.Editor;

public class ConnectorViewModel : ViewModelBase
{
    private Point _anchor;

    public ConnectorViewModel()
    {
    }

    public ConnectorViewModel(OperationData data, Guid id)
    {
        Data = data;
        Id = id;
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

    public Guid Id { get; set; }

    private OperationData _data;

    public OperationData Data
    {
        get => _data;
        set => SetProperty(ref _data, value);
    }
}

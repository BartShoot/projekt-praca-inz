using NoodleCV;
using PrototypeWPF.Utilities;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeWPF;

public class PendingConnectionViewModel
{
    private readonly EditorViewModel _editor;
    private ConnectorViewModel _source;

    public PendingConnectionViewModel(EditorViewModel editor)
    {
        _editor = editor;
        StartCommand = new DelegateCommand<ConnectorViewModel>(source => _source = source);
        FinishCommnd = new DelegateCommand<ConnectorViewModel>(target =>
        {
            if (target != null)
            {
                _editor.Connect(_source, target);
            }
        });
    }

    public ICommand StartCommand { get; }
    public ICommand FinishCommnd { get; }
}

public class ConnectionViewModel
{
    public ConnectorViewModel Source { get; }
    public ConnectorViewModel Target { get; }

    public ConnectionViewModel(ConnectorViewModel source, ConnectorViewModel target)
    {
        Source = source;
        Target = target;

        Source.IsConnected = true;
        Target.IsConnected = true;
    }
}

public class ConnectorViewModel : INotifyPropertyChanged
{
    private System.Windows.Point _anchor;

    public ConnectorViewModel()
    {
    }

    public ConnectorViewModel(OperationData data)
    {
        _data = data;
    }

    public System.Windows.Point Anchor
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

public class NodeViewModel
{
    public string Title { get; set; }

    public ObservableCollection<ConnectorViewModel> Input { get; set; }

    public ObservableCollection<ConnectorViewModel> Output { get; set; }

    private System.Windows.Point _location;

    public IOperationViewModel OperationViewModel
    {
        get;
    }

    public System.Windows.Point Location
    {
        get { return _location; }
        set
        {
            _location = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public NodeViewModel(IOperationViewModel operationViewModel)
    {
        OperationViewModel = operationViewModel;
        Title = operationViewModel.Name;
        Input = new ObservableCollection<ConnectorViewModel>(OperationViewModel.NodeInput.Select(x => new ConnectorViewModel(x)));
        Output = new ObservableCollection<ConnectorViewModel>(OperationViewModel.Operation.Outputs.Select(x => new ConnectorViewModel(x)));
    }
}

public class EditorViewModel
{
    public ObservableCollection<NodeViewModel> Nodes { get; } 
        = new ObservableCollection<NodeViewModel>();

    public ObservableCollection<NodeViewModel> SelectedNodes { get; }
        = new ObservableCollection<NodeViewModel>();

    public ObservableCollection<ConnectionViewModel> Connections { get; } =
        new ObservableCollection<ConnectionViewModel>();

    public PendingConnectionViewModel PendingConnection { get; }
    public ICommand DisconnectConnectorCommand { get; }
    public ContextMenu ItemContextMenu { get; } = new ContextMenu();
    public EditorViewModel(IReadOnlyList<IOperationViewModel> allOperations)
    {
        PendingConnection = new PendingConnectionViewModel(this);

        DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
        {
            var connection = Connections.First(x => x.Source == connector || x.Target == connector);
            connection.Source.IsConnected = false;
            connection.Target.IsConnected = false;
            Connections.Remove(connection);
        });

        SelectedNodes.CollectionChanged += SelectedNodesChanged;
        InitializeMenu(allOperations);
    }
    
    public void Connect(ConnectorViewModel source, ConnectorViewModel target)
    {
        Connections.Add(new ConnectionViewModel(source, target));
    }

    private void InitializeMenu(IReadOnlyList<IOperationViewModel> allOperations)
    {
        foreach (var operation in allOperations)
        {
            var menuItem = new MenuItem { Header = operation.Name };
            menuItem.Click += add;
            ItemContextMenu.Items.Add(menuItem);
            continue;

            void add(object sender, RoutedEventArgs e)
            {
                Nodes.Add(new NodeViewModel(operation));
            }
        }
    }

    private void SelectedNodesChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {

    }
}
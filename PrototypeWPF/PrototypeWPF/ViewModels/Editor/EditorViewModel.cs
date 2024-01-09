using PrototypeWPF.Model;
using PrototypeWPF.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeWPF.ViewModels.Editor;
public class EditorViewModel : ViewModelBase
{
    public ObservableCollection<NodeViewModel> Nodes { get; }
        = new ObservableCollection<NodeViewModel>();

    private NodeViewModel _selectedNode;
    public NodeViewModel SelectedNode
    {
        get => _selectedNode;
        set => SetProperty(ref _selectedNode, value);
    }

    private NodeViewModel _pinnedNode1;

    public NodeViewModel PinnedNode1
    {
        get => _pinnedNode1;
        set => SetProperty(ref _pinnedNode1, value);
    }
    private NodeViewModel _pinnedNode2;

    public NodeViewModel PinnedNode2
    {
        get => _pinnedNode2;
        set => SetProperty(ref _pinnedNode2, value);
    }

    public ICommand PinNode1 { get; }
    public ICommand PinNode2 { get; }
    public ICommand DeleteSelected { get; }

    public ObservableCollection<ConnectionViewModel> Connections { get; } =
        new ObservableCollection<ConnectionViewModel>();

    public PendingConnectionViewModel PendingConnection { get; }
    public ICommand DisconnectConnectorCommand { get; }
    public ContextMenu ItemContextMenu { get; } = new ContextMenu();
    public EditorViewModel(IReadOnlyList<OperationDescriptor> allOperations)
    {
        PendingConnection = new PendingConnectionViewModel(this);

        DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
        {
            var connection = Connections.First(x => x.Source == connector || x.Target == connector);
            if (Connections.Count.Equals(1))
            {
                connection.Source.IsConnected = false;
            }
            connection.Target.IsConnected = false;
            Connections.Remove(connection);
        });

        PinNode1 = new DelegateCommand(() => PinnedNode1 = SelectedNode);
        PinNode2 = new DelegateCommand(() => PinnedNode2 = SelectedNode);
        DeleteSelected = new DelegateCommand(() => Nodes.Remove(SelectedNode));

        Nodes.CollectionChanged += NodesChangedHandler;
        Connections.CollectionChanged += ConnectionsChangedHandler;

        InitializeMenu(allOperations);
    }

    private void ConnectionsChangedHandler(object? sender, NotifyCollectionChangedEventArgs e)
    {
        NodesChangedHandler(sender, e);
    }

    private void NodesChangedHandler(object? sender, NotifyCollectionChangedEventArgs e)
    {
        foreach (var node in Nodes)
        {
            node.OperationViewModel.Operation.Execute();
        }
        RaisePropertyChanged(nameof(PinnedNode1));
        RaisePropertyChanged(nameof(PinnedNode2));
    }

    public void Connect(ConnectorViewModel source, ConnectorViewModel target)
    {
        target.Data = source.Data;
        Connections.Add(new ConnectionViewModel(source, target));
    }

    private void InitializeMenu(IReadOnlyList<OperationDescriptor> allOperations)
    {
        foreach (var operation in allOperations)
        {
            var menuItem = new MenuItem { Header = operation.Name };
            menuItem.Click += add;
            ItemContextMenu.Items.Add(menuItem);
            continue;

            void add(object sender, RoutedEventArgs e)
            {
                if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
                {
                    var target = contextMenu.PlacementTarget; // This is your ListView in this case
                    var mousePositionRelativeToTarget = Mouse.GetPosition(target);
                    var viewModel = operation.CreateViewModel();
                    Nodes.Add(new NodeViewModel(operation.Name, viewModel, mousePositionRelativeToTarget));

                    viewModel.PropertyChanged += (sender, e) =>
                    {
                        RaisePropertyChanged(nameof(operation));
                        RaisePropertyChanged(nameof(Nodes));
                    };
                }
            }
        }
    }
}
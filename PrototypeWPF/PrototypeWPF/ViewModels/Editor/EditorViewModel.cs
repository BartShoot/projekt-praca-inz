using PrototypeWPF.Utilities;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public ObservableCollection<ConnectionViewModel> Connections { get; } =
        new ObservableCollection<ConnectionViewModel>();

    public PendingConnectionViewModel PendingConnection { get; }
    public ICommand DisconnectConnectorCommand { get; }
    public ContextMenu ItemContextMenu { get; } = new ContextMenu();
    public EditorViewModel(IReadOnlyList<OperationViewModel> allOperations)
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

        InitializeMenu(allOperations);
    }

    public void Connect(ConnectorViewModel source, ConnectorViewModel target)
    {
        Connections.Add(new ConnectionViewModel(source, target));
    }

    private void InitializeMenu(IReadOnlyList<OperationViewModel> allOperations)
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
}
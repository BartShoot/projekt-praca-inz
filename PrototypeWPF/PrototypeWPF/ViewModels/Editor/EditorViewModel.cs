using Microsoft.Win32;
using Newtonsoft.Json;
using OpenCvSharp;
using PrototypeWPF.Model;
using PrototypeWPF.Utilities;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PrototypeWPF.ViewModels.Editor;
public class EditorViewModel : ViewModelBase
{
    #region props
    public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();

    public ObservableCollection<ConnectionViewModel> Connections { get; } = new ObservableCollection<ConnectionViewModel>();

    public ContextMenu ItemContextMenu { get; } = new ContextMenu();

    public PendingConnectionViewModel PendingConnection { get; }

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
        set => SetProperty(ref _pinnedNode1, value, () =>
        {
            PinnedOperationChangedHandler();
            RaisePropertyChanged(nameof(PinnedOperation1));
        }
        );
    }
    public IOperationViewModel? PinnedOperation1 => PinnedNode1?.OperationViewModel;

    private NodeViewModel _pinnedNode2;
    public NodeViewModel PinnedNode2
    {
        get => _pinnedNode2;
        set => SetProperty(ref _pinnedNode2, value, () =>
        {
            PinnedOperationChangedHandler();
            RaisePropertyChanged(nameof(PinnedOperation2));
        }
        );
    }
    public IOperationViewModel? PinnedOperation2 => PinnedNode2?.OperationViewModel;
    #endregion

    #region commands
    public ICommand PinNode1Command { get; }
    public ICommand PinNode2Command { get; }
    public ICommand DeleteSelectedCommand { get; }
    public ICommand DisconnectConnectorCommand { get; }
    public IReadOnlyList<OperationDescriptor> AllOperations { get; }
    #endregion

    public EditorViewModel(IReadOnlyList<OperationDescriptor> allOperations)
    {
        PendingConnection = new PendingConnectionViewModel(this);

        DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(DisconnectConnector);
        DeleteSelectedCommand = new DelegateCommand(DeleteSelected);
        PinNode1Command = new DelegateCommand(() => PinnedNode1 = SelectedNode);
        PinNode2Command = new DelegateCommand(() => PinnedNode2 = SelectedNode);

        Connections.CollectionChanged += ConnectionsChangedHandler;
        InitializeMenu(allOperations);
        AllOperations = allOperations;
    }

    private void UpdateConnections(object? sender, PropertyChangedEventArgs e)
    {
        var node = sender as NodeViewModel;
        foreach (var con in Connections)
        {
            Nodes.Where(n => n.Input.Contains(con.Target)).ToList().ForEach(n =>
                   {
                       n.OperationViewModel.NodeInput[0].Set(n.Input[0].Data.Get<Mat>());
                       n.OperationViewModel.Execute();
                       if (n.Output.Count > 0)
                           n.Output[0].Data.Set(n.OperationViewModel.Operation.Outputs[0].Get<Mat>());
                       RaisePropertyChanged(nameof(n.OperationViewModel));
                       RaisePropertyChanged(nameof(con.Target));
                       RaisePropertyChanged(nameof(PinnedOperation2));
                   });
        }
    }

    private void PinnedOperationChangedHandler()
    {
        ConnectionsChangedHandler(new object(), new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
                    var target = contextMenu.PlacementTarget;
                    var mousePositionRelativeToTarget = Mouse.GetPosition(target);
                    var viewModel = operation.CreateViewModel();
                    var node = new NodeViewModel(operation.Name, viewModel, mousePositionRelativeToTarget);
                    node.PropertyChanged += UpdateConnections;
                    Nodes.Add(node);

                    viewModel.PropertyChanged += (sender, e) =>
                    {
                        RaisePropertyChanged(nameof(operation));
                        RaisePropertyChanged(nameof(Nodes));
                    };
                }
            }
        }
    }

    private void DeleteSelected()
    {
        if (PinnedNode1 != null && PinnedNode1.Equals(SelectedNode))
        {
            PinnedNode1 = null;
        }
        if (PinnedNode2 != null && PinnedNode2.Equals(SelectedNode))
        {
            PinnedNode2 = null;
        }

        foreach (var input in SelectedNode.Input)
        {
            if (input.IsConnected)
            {
                DisconnectConnectorCommand.Execute(input);
            }
        }
        foreach (var output in SelectedNode.Output)
        {
            if (output.IsConnected)
            {
                DisconnectConnectorCommand.Execute(output);
            }
        }
        Nodes.Remove(SelectedNode);
    }

    private void DisconnectConnector(ConnectorViewModel connector)
    {
        var connection = Connections.Where(x => x.Source == connector || x.Target == connector).ToList();
        foreach (var node in connection)
        {
            if (connection.Count.Equals(1))
            {
                connection[0].Source.IsConnected = false;
            }
            node.Target.IsConnected = false;
            Connections.Remove(node);
        }
    }

    public void Connect(ConnectorViewModel source, ConnectorViewModel target)
    {
        if (source.Equals(target))
            return;
        if (source.Id.Equals(target.Id))
            return;
        target.Data = source.Data;
        Connections.Add(new ConnectionViewModel(source, target));
    }

    private void ConnectionsChangedHandler(object? sender, NotifyCollectionChangedEventArgs e)
    {
        foreach (var node in Nodes)
        {
            var test = node.OperationViewModel.Operation.Execute();
            if (test.Status.Equals(false))
            {
                string errors = string.Join(", ", test.Errors);
                errors += '.';
                var mainWindow = Application.Current.MainWindow as MainWindow;
                var editor = mainWindow.EditorViewName;
                (editor)?.RootSnackbar.Show(
                    "Can't calculate " + node.Title, errors,
                    Wpf.Ui.Common.SymbolRegular.TextBulletListSquareWarning16, Wpf.Ui.Common.ControlAppearance.Danger);
                break;
            }
            Connections.Where(c => node.Output.Contains(c.Source)).ToList()
                .ForEach(con => con = new ConnectionViewModel(con.Source, con.Target));
        }
        RaisePropertyChanged(nameof(PinnedNode1));
        RaisePropertyChanged(nameof(PinnedNode2));
    }

    public void NewFile()
    {
        Nodes.Clear();
        Connections.Clear();
        PinnedNode1 = null;
        PinnedNode2 = null;
    }

    public void SaveFile()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "JSON file (*.json)|*.json";
        if (saveFileDialog.ShowDialog() == true)
        {
            var appState = new
            {
                this.Nodes,
                this.Connections,
                this.SelectedNode,
                this.PinnedNode1,
                this.PinnedNode2,
            };
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.Auto;

            string jsonData = JsonConvert.SerializeObject(appState, Formatting.Indented, serializerSettings);
            File.WriteAllText(saveFileDialog.FileName, jsonData);
        };
    }

    public void OpenFile()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true)
        {
            string filePath = openFileDialog.FileName;
            if (!File.Exists(filePath))
                return;
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.TypeNameHandling = TypeNameHandling.Auto;

            string jsonData = File.ReadAllText(filePath);
            var appState = JsonConvert.DeserializeObject<AppState>(jsonData, serializerSettings);

            this.Nodes.Clear();
            foreach (var node in appState.Nodes)
            {
                var vm = AllOperations.Where(op =>
                op.Name.Equals(node.Title)).First().CreateViewModel();
                vm.CopyParameter(node.OperationViewModel);
                Nodes.Add(new NodeViewModel(node.Title, vm, node.Location, node.Id));
            }

            this.Connections.Clear();
            foreach (var connection in appState.Connections)
            {
                var node1 = Nodes.Where(n => n.Id.Equals(connection.Source.Id)).First();
                var node2 = Nodes.Where(n => n.Id.Equals(connection.Target.Id)).First();
                Connect(node1.Output[0], node2.Input[0]);
            }

            if (appState.SelectedNode != null)
                this.SelectedNode = Nodes.Where(n => n.Id.Equals(appState.SelectedNode.Id)).First();
            if (appState.PinnedNode1 != null)
                this.PinnedNode1 = Nodes.Where(n => n.Id.Equals(appState.PinnedNode1.Id)).First();
            if (appState.PinnedNode2 != null)
                this.PinnedNode2 = Nodes.Where(n => n.Id.Equals(appState.PinnedNode2.Id)).First();
        }
    }

    private class AppState
    {
        public ObservableCollection<NodeViewModel> Nodes { get; set; }
        public ObservableCollection<ConnectionViewModel> Connections { get; set; }
        public NodeViewModel SelectedNode { get; set; }
        public NodeViewModel PinnedNode1 { get; set; }
        public NodeViewModel PinnedNode2 { get; set; }
    }

}
using OpenCvSharp;
using PrototypeWPF.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using PrototypeWPF.ViewModels.Operations;
using Point = System.Windows.Point;

namespace PrototypeWPF
{
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
        public ObservableCollection<NodeViewModel> Nodes { get; } = new ObservableCollection<NodeViewModel>();

        public ObservableCollection<ConnectionViewModel> Connections { get; } =
            new ObservableCollection<ConnectionViewModel>();

        public PendingConnectionViewModel PendingConnection { get; }
        public ICommand DisconnectConnectorCommand { get; }
        public ContextMenu ItemContextMenu { get; set; }

        public EditorViewModel()
        {
            InitializeMenu();

            PendingConnection = new PendingConnectionViewModel(this);

            DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
            {
                var connection = Connections.First(x => x.Source == connector || x.Target == connector);
                connection.Source.IsConnected = false;
                connection.Target.IsConnected = false;
                Connections.Remove(connection);
            });
        }

        private void InitializeMenu()
        {
            ItemContextMenu = new ContextMenu();
            MenuItem addSelectImage = new MenuItem { Header = "Select image" };
            addSelectImage.Click += AddSelectImage_Click;
            ItemContextMenu.Items.Add(addSelectImage);

            MenuItem addResize = new MenuItem { Header = "Resize" };
            addResize.Click += AddResize_Click;
            ItemContextMenu.Items.Add(addResize);
        }

        private void AddSelectImage_Click(object sender, RoutedEventArgs e)
        {
            var loadImageViewModel = new LoadImageViewModel();
            loadImageViewModel.LoadImage();
            
            Nodes.Add(new NodeViewModel(loadImageViewModel)
            {
                Output = new ObservableCollection<ConnectorViewModel>()
                {
                    new ConnectorViewModel()
                    {
                        Data = loadImageViewModel.Operation.Outputs[0]
                    }
            }
            });
        }

        private void AddResize_Click(object sender, RoutedEventArgs e)
        {
            Nodes.Add(new NodeViewModel(new ResizeViewModel()));
        }

        public void Connect(ConnectorViewModel source, ConnectorViewModel target)
        {
            Connections.Add(new ConnectionViewModel(source, target));
        }
    }
}
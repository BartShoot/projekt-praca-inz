using PrototypeWPF.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private Point _anchor;

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
    }

    public class NodeViewModel
    {
        public string Title { get; set; }

        public ObservableCollection<ConnectorViewModel> Input { get; set; } =
            new ObservableCollection<ConnectorViewModel>();

        public ObservableCollection<ConnectorViewModel> Output { get; set; } =
            new ObservableCollection<ConnectorViewModel>();

        private Point _location;

        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
            ItemContextMenu = new ContextMenu();
            MenuItem addNodeMenuItem = new MenuItem { Header = "Add node" };
            addNodeMenuItem.Click += AddNodeMenuItem_Click;
            ItemContextMenu.Items.Add(addNodeMenuItem);
            PendingConnection = new PendingConnectionViewModel(this);
            var welcome = new NodeViewModel
            {
                Title = "Welcome",
                Input = new ObservableCollection<ConnectorViewModel>
                {
                    new ConnectorViewModel
                    {
                        Title = "In"
                    }
                },
                Output = new ObservableCollection<ConnectorViewModel>
                {
                    new ConnectorViewModel
                    {
                        Title = "Out"
                    }
                }
            };
            Nodes.Add(welcome);
            var nodify = new NodeViewModel
            {
                Title = "To Nodify",
                Input = new ObservableCollection<ConnectorViewModel>
                {
                    new ConnectorViewModel
                    {
                        Title = "In"
                    }
                }
            };
            Nodes.Add(nodify);
            Connections.Add(new ConnectionViewModel(welcome.Output[0], nodify.Input[0]));
            DisconnectConnectorCommand = new DelegateCommand<ConnectorViewModel>(connector =>
            {
                var connection = Connections.First(x => x.Source == connector || x.Target == connector);
                connection.Source.IsConnected = false;
                connection.Target.IsConnected = false;
                Connections.Remove(connection);
            });
        }

        private void AddNodeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Nodes.Add(new NodeViewModel
            {
                Title = "New",
                Input = new ObservableCollection<ConnectorViewModel> { new ConnectorViewModel { Title = "in" } },
                Location = new Point(50, 50)
            });
        }

        public void Connect(ConnectorViewModel source, ConnectorViewModel target)
        {
            Connections.Add(new ConnectionViewModel(source, target));
        }
    }
}
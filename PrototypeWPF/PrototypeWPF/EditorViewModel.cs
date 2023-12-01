using OpenCvSharp;
using PrototypeWPF.Operations;
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
        private System.Windows.Point _anchor;

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
        private Mat _image;

        public Mat Image
        {
            get { return _image; }
            set
            {
                _image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }

    }

    public class NodeViewModel
    {
        public string Title { get; set; }

        public ObservableCollection<ConnectorViewModel> Input
        {
            get => input; set
            {
                input = value;
                if (Output.Count == 0)
                {
                    Output.Add(new ConnectorViewModel());
                }
                if (Input.Count > 0 && Input[0].Image != null)
                {
                    Output[0].Image = operation.GetFunc();
                }
            }
        }
        public ObservableCollection<ConnectorViewModel> Output { get; set; } =
            new ObservableCollection<ConnectorViewModel>();

        private System.Windows.Point _location;
        private ObservableCollection<ConnectorViewModel> input = new ObservableCollection<ConnectorViewModel>();
        private readonly IOperation operation;

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

        public NodeViewModel(IOperation operation)
        {
            this.operation = operation;
            Title = operation.Name;
            Input = new ObservableCollection<ConnectorViewModel>()
                {
                new ConnectorViewModel()
                {
                    Title = "Image"
                }
            };
            Output = new ObservableCollection<ConnectorViewModel>()
                {
                new ConnectorViewModel()
                {
                    Title = "Image"
                }
            };
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
            var select = new SelectImage();
            Nodes.Add(new NodeViewModel(select)
            {
                Output = new ObservableCollection<ConnectorViewModel>()
                {
                    new ConnectorViewModel()
                    {
                        Image = select.GetFunc()
                    }
            }
            });
        }

        private void AddResize_Click(object sender, RoutedEventArgs e)
        {
            Nodes.Add(new NodeViewModel(new Resize()));
        }

        public void Connect(ConnectorViewModel source, ConnectorViewModel target)
        {
            Connections.Add(new ConnectionViewModel(source, target));
        }
    }
}
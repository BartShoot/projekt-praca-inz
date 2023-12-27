using PrototypeWPF.ViewModels.Operations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PrototypeWPF.ViewModels.Editor;

public class NodeViewModel
{
    public string Title { get; set; }

    public ObservableCollection<ConnectorViewModel> Input { get; set; }

    public ObservableCollection<ConnectorViewModel> Output { get; set; }

    private Point _location;

    public OperationViewModel OperationViewModel
    {
        get;
    }

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

    public NodeViewModel(OperationViewModel operationViewModel)
    {
        OperationViewModel = operationViewModel;
        Title = operationViewModel.Name;
        Input = new ObservableCollection<ConnectorViewModel>(OperationViewModel.NodeInput.Select(x => new ConnectorViewModel(x)));
        Output = new ObservableCollection<ConnectorViewModel>(OperationViewModel.Operation.Outputs.Select(x => new ConnectorViewModel(x)));
    }
}

using OpenCvSharp;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PrototypeWPF.ViewModels.Editor;

public class NodeViewModel : ViewModelBase
{
    public string Title { get; set; }

    public ObservableCollection<ConnectorViewModel> Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public ObservableCollection<ConnectorViewModel> Output { get => _output; set => SetProperty(ref _output, value); }

    private System.Windows.Point _location;
    private ObservableCollection<ConnectorViewModel> _input;
    private ObservableCollection<ConnectorViewModel> _output;

    public OperationViewModel OperationViewModel
    {
        get;
    }

    public System.Windows.Point Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }

    public NodeViewModel(OperationViewModel operationViewModel)
    {
        OperationViewModel = operationViewModel;
        Title = operationViewModel.Name;
        Input = new ObservableCollection<ConnectorViewModel>(OperationViewModel.NodeInput.Select(x => new ConnectorViewModel(x)));
        Output = new ObservableCollection<ConnectorViewModel>(OperationViewModel.Operation.Outputs.Select(x => new ConnectorViewModel(x)));

        foreach (var item in Input)
        {
            item.PropertyChanged += InputPropertyChangedHandler;
        }
        Input.CollectionChanged += (sender, e) =>
        {
            OperationViewModel.NodeInput[0].Set(Input[0].Data.Get<Mat>());
        };
    }

    private void InputPropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        if (OperationViewModel.NodeInput[0].Get<Mat>() != (sender as ConnectorViewModel).Data.Get<Mat>())
        {
            OperationViewModel.NodeInput[0].Set((sender as ConnectorViewModel).Data.Get<Mat>());
            OperationViewModel.Execute();
            RaisePropertyChanged(nameof(Input));
            RaisePropertyChanged(nameof(NodeViewModel));
        }
    }
}

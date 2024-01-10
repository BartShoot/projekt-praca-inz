using OpenCvSharp;
using PrototypeWPF.ViewModels.Operations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PrototypeWPF.ViewModels.Editor;

public class NodeViewModel : ViewModelBase
{
    public string Title { get; set; }
    public Guid Id { get; private set; }

    public ObservableCollection<ConnectorViewModel> Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    public ObservableCollection<ConnectorViewModel> Output
    {
        get => _output;
        set => SetProperty(ref _output, value);
    }

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

    public NodeViewModel(string name, OperationViewModel operationViewModel, System.Windows.Point placement)
    {
        Title = name;
        OperationViewModel = operationViewModel;
        Guid Id = Guid.NewGuid();
        Input = new ObservableCollection<ConnectorViewModel>(OperationViewModel.NodeInput.Select(x => new ConnectorViewModel(x, Id)));
        Output = new ObservableCollection<ConnectorViewModel>(OperationViewModel.Operation.Outputs.Select(x => new ConnectorViewModel(x, Id)));
        Location = placement;
        foreach (var item in Input)
        {
            item.PropertyChanged += InputNodeChangedHandler;
        }
        foreach (var item in Output)
        {
            item.PropertyChanged += OutputNodeChangedHandler;
        }
    }

    private void OutputNodeChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        RaisePropertyChanged(nameof(Output));
        RaisePropertyChanged(nameof(NodeViewModel));
    }

    private void InputChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        OperationViewModel.Execute();
        RaisePropertyChanged(nameof(NodeViewModel));
    }

    private void InputNodeChangedHandler(object? sender, PropertyChangedEventArgs e)
    {
        if ((sender as ConnectorViewModel) != null && (sender as ConnectorViewModel).IsConnected == true)
        {
            if (OperationViewModel.NodeInput.Count > 0)
            {
                OperationViewModel.NodeInput[0].Set((sender as ConnectorViewModel).Data.Get<Mat>());
            }
            RaisePropertyChanged(nameof(Input));
            InputChangedHandler(sender, e);
        }
    }
}

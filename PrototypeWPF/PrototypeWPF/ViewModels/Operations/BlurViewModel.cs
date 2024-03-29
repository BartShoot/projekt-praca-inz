using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using OpenCvSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PrototypeWPF.ViewModels.Operations;

public class BlurViewModel : OperationViewModel, IOperationViewModel
{
    public BlurViewModel()
    {
        Operation = new Blur();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1],
            Operation.Inputs[2]
        };

        foreach (var input in NodeInput)
        {
            input.PropertyChanged += HandleChangedNodeInput;
        }

        NodeInput.CollectionChanged += (sender, e) =>
        {
            InputImage = NodeInput[0].Get<Mat>();
        };
    }

    private void HandleChangedNodeInput(object? sender, PropertyChangedEventArgs e)
    {
        Operation.Inputs[0].Set(NodeInput[0].Get<Mat>());
        RaisePropertyChanged(nameof(NodeInput));
    }

    private int _size;
    public int Size
    {
        get => Operation.Inputs[1].Get<int>();
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _size, value);
        }
    }

    private double _strength;
    public double Strength
    {
        get => Operation.Inputs[2].Get<double>();
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _strength, value);
        }
    }

    private Mat _inputImage;

    public Mat InputImage
    {
        get => _inputImage;
        set
        {
            Operation.Inputs[0].Set(value);
            SetProperty(ref _inputImage, value);
        }
    }

    public void CopyParameter(IOperationViewModel operationViewModel)
    {
        var vm = operationViewModel as BlurViewModel;
        Strength = vm.Strength;
        Size = vm.Size;
    }
}
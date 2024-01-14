using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class ResizeViewModel : OperationViewModel, IOperationViewModel
{
    public ResizeViewModel()
    {
        Operation = new Resize();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1],
            Operation.Inputs[2]
        };
    }

    private int _sizeX;
    public int SizeX
    {
        get => Operation.Inputs[1].Get<int>();
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _sizeX, value);
        }
    }

    private int _sizeY;
    public int SizeY
    {
        get => Operation.Inputs[2].Get<int>();
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _sizeY, value);
        }
    }

    public void CopyParameter(IOperationViewModel operationViewModel)
    {
        var vm = operationViewModel as ResizeViewModel;
        SizeX = vm.SizeX;
        SizeY = vm.SizeY;
    }
}
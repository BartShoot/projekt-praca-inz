using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class ResizeViewModel : OperationViewModel
{
    public ResizeViewModel()
    {
        Name = "Resize";
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
        get => _sizeX;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _sizeX, value);
        }
    }

    private int _sizeY;
    public int SizeY
    {
        get => _sizeY;
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _sizeY, value);
        }
    }
}
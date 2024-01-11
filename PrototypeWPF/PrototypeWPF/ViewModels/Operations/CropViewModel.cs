using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class CropViewModel : OperationViewModel
{
    public CropViewModel()
    {
        Operation = new Crop();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1],
            Operation.Inputs[2],
            Operation.Inputs[3],
            Operation.Inputs[4]
        };
    }
    private int _startX;
    public int StartX
    {
        get => _startX;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _startX, value);
        }
    }

    private int _startY;
    public int StartY
    {
        get => _startY;
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _startY, value);
        }
    }

    private int _width;
    public int Width
    {
        get => _width;
        set
        {
            Operation.Inputs[3].Set(value);
            SetProperty(ref _width, value);
        }
    }

    private int _height;
    public int Height
    {
        get => _height;
        set
        {
            Operation.Inputs[4].Set(value);
            SetProperty(ref _height, value);
        }
    }
}

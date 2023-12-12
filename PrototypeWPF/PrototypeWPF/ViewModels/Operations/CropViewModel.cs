using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels.Operations;

public class CropViewModel : ViewModelBase, IOperationViewModel
{
    public CropViewModel()
    {
        Operation = new Crop();
        NodeInput = new List<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new List<OperationData>
        {
            Operation.Inputs[1],
            Operation.Inputs[2],
            Operation.Inputs[3],
            Operation.Inputs[4]
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

    public string Name { get; } = "Crop";

    public IOperation Operation { get; }

    public IReadOnlyList<OperationData> NodeInput { get; }

    public IReadOnlyList<OperationData> NodeParameter { get; }
}

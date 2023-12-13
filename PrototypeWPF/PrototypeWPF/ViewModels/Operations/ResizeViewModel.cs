using System.Collections.Generic;
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;

namespace PrototypeWPF.ViewModels.Operations;

public class ResizeViewModel: ViewModelBase, IOperationViewModel
{    
    public ResizeViewModel()
    {
        Operation = new Resize();
        NodeInput = new List<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new List<OperationData>
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

    public string Name { get; }  = "Resize";
    public IOperation Operation { get; } 
    public IReadOnlyList<OperationData> NodeInput { get; }
    public IReadOnlyList<OperationData> NodeParameter { get; }
}
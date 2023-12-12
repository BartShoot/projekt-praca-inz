using System.Collections.Generic;
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;

namespace PrototypeWPF.ViewModels.Operations;

public class BlurViewModel: ViewModelBase, IOperationViewModel
{
    public BlurViewModel()
    {
        Operation = new Blur();
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
    
    private int _size;
    public int Size
    {
        get => _size;
        set => SetProperty(ref _size, value);
    }
    
    private int _strength;
    public int Strength
    {
        get => _strength;
        set => SetProperty(ref _strength, value);
    }

    public string Name { get; } = "Blur";
    public IOperation Operation { get; }

    public IReadOnlyList<OperationData> NodeInput { get; }
    
    public IReadOnlyList<OperationData> NodeParameter { get; }
}
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels.Operations;

public class BlurViewModel : OperationViewModel
{
    public BlurViewModel()
    {
        Name = "Blur";
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
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _size, value);
        }
    }

    private int _strength;
    public int Strength
    {
        get => _strength;
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _strength, value);
        }
    }
}
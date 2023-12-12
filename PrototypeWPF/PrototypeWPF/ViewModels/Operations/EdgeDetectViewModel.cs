using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels.Operations;

public class EdgeDetectViewModel : ViewModelBase, IOperationViewModel
{
    public EdgeDetectViewModel()
    {
        Operation = new EdgeDetect();
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
    private double _thresholdLower;

    public double ThresholdLower
    {
        get => _thresholdLower;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _thresholdLower, value);
        }
    }
    private double _thresholdUpper;

    public double ThresholdUpper
    {
        get => _thresholdUpper;
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _thresholdUpper, value);
        }
    }

    public string Name { get; } = "Edge detect";

    public IOperation Operation { get; }

    public IReadOnlyList<OperationData> NodeInput { get; }

    public IReadOnlyList<OperationData> NodeParameter { get; }
}

using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class EdgeDetectViewModel : OperationViewModel
{
    public EdgeDetectViewModel()
    {
        Name = "Edge detect";
        Operation = new EdgeDetect();
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
}

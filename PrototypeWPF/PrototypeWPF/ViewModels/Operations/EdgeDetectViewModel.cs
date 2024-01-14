using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class EdgeDetectViewModel : OperationViewModel, IOperationViewModel
{
    public EdgeDetectViewModel()
    {
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
        get => Operation.Inputs[1].Get<double>();
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _thresholdLower, value);
        }
    }
    private double _thresholdUpper;

    public double ThresholdUpper
    {
        get => Operation.Inputs[2].Get<double>();
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _thresholdUpper, value);
        }
    }

    public void CopyParameter(IOperationViewModel operationViewModel)
    {
        var vm = operationViewModel as EdgeDetectViewModel;
        ThresholdLower = vm.ThresholdLower;
        ThresholdUpper = vm.ThresholdUpper;
    }
}

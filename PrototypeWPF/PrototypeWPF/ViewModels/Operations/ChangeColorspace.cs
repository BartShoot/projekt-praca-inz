using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using OpenCvSharp;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels.Operations;

public class ChangeColorspaceViewModel : OperationViewModel
{
    public ChangeColorspaceViewModel()
    {
        Name = "ChangeColorspace";
        Operation = new ChangeColorspace();
        NodeInput = new List<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new List<OperationData>
        {
            Operation.Inputs[1]
        };
    }
    private ColorConversionCodes _conversionCodes;

    public ColorConversionCodes ConversionCodes
    {
        get => _conversionCodes;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _conversionCodes, value);
        }
    }
}

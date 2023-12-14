using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;
public class ChangeColorspace : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>()
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<ColorConversionCodes>()
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>()
    {
        OperationOutput.Create<Mat>(),
    };

    public Result Execute()
    {
        var conversionCodes = Inputs[1].Get<ColorConversionCodes>();

        var output = Inputs[0].Get<Mat>().CvtColor(conversionCodes);

        Outputs[0].Set(output);
        return Result.Ok();
    }
}
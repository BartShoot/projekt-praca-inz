using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class EdgeDetect : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create(80),
        OperationInput.Create(120),
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>(),
    };

    public Result Execute()
    {
        var thresholdLower = Inputs[1].Get<double>();
        var thresholdUpper = Inputs[2].Get<double>();

        var output = Inputs[0].Get<Mat>().Canny(thresholdLower, thresholdUpper);

        Outputs[0].Set(output);
        return Result.Ok();
    }
}

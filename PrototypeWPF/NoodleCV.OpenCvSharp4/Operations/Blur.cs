using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Blur : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<int>(),
        OperationInput.Create<double>()
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public void Execute()
    {
        var image = Inputs[0].Get<Mat>();
        var size = Inputs[1].Get<int>();
        var strength = Inputs[2].Get<double>();

        var output = new Mat();
        Cv2.GaussianBlur(image, output, new Size(size, size), strength);

        Outputs[0].Set(output);
    }
}
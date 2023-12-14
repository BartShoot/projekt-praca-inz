using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Resize : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<int>(),
        OperationInput.Create<int>()
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public Result Execute()
    {
        var image = Inputs[0].Get<Mat>();
        var sizeX = Inputs[1].Get<int>();
        var sizeY = Inputs[2].Get<int>();

        var output = new Mat();
        // TODO: add options to switch between relative(% of image) and absolute resize + interpolation options
        Cv2.Resize(image, output, new Size(sizeX, sizeY), interpolation: InterpolationFlags.Cubic);

        Outputs[0].Set(output);
        return new Result();
    }
}

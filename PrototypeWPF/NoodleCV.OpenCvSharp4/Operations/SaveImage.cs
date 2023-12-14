using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class SaveImage : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<string>()
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>();

    public Result Execute()
    {
        var output = Inputs[0].Get<Mat>();
        var imagePath = Inputs[1].Get<string>();

        Cv2.ImWrite(imagePath, output);
        return new Result();
    }
}
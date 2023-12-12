using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class LoadImage : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<string>()
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public void Execute()
    {
        var imagePath = Inputs[0].Get<string>();

        var output = Cv2.ImRead(imagePath);

        Outputs[0].Set(output);
    }
}
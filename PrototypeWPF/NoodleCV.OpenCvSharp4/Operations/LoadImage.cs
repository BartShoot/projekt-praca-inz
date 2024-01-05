using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class LoadImage : IOperation
{
    public List<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<string>()
    };

    public List<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public Result Execute()
    {
        var imagePath = Inputs[0].Get<string>();

        if (imagePath != null)
        {
            var output = Cv2.ImRead(imagePath);

            Outputs[0].Set(output);
            return Result.Ok();
        }
        return Result.Error("gowno");
    }
}
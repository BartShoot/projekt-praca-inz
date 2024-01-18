using FluentValidation.Results;
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

        ValidationResult result = ValidateInputs();
        if (!result.IsValid)
        {
            return Result.Error(result.Errors);
        }
        var output = Cv2.ImRead(imagePath);

        Outputs[0].Set(output);
        return Result.Ok();
    }
    private ValidationResult ValidateInputs()
    {
        var validator = new LoadImageValidator();
        return validator.Validate(Inputs);
    }
}
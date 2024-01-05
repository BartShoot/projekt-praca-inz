using FluentValidation.Results;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class SaveImage : IOperation
{
    public List<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<string>()
    };

    public List<OperationOutput> Outputs { get; } = new List<OperationOutput>();

    public Result Execute()
    {
        var output = Inputs[0].Get<Mat>();
        var imagePath = Inputs[1].Get<string>();
        ValidationResult validationResult = ValidateInputs();
        if (!validationResult.IsValid)
        {
            return Result.Error(validationResult.Errors);
        }

        Cv2.ImWrite(imagePath, output);
        return Result.Ok();
    }
    private ValidationResult ValidateInputs()
    {
        var validator = new SaveImageValidator();
        return validator.Validate(Inputs);
    }

}
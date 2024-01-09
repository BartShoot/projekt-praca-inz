using FluentValidation.Results;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Resize : IOperation
{
    public List<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create(128),
        OperationInput.Create(128)
    };

    public List<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public Result Execute()
    {
        var image = Inputs[0].Get<Mat>();
        var sizeX = Inputs[1].Get<int>();
        var sizeY = Inputs[2].Get<int>();


        ValidationResult validationResult = ValidateInputs();
        if (!validationResult.IsValid)
        {
            return Result.Error(validationResult.Errors);
        }

        var output = new Mat();
        // TODO: add options to switch between relative(% of image) and absolute resize + interpolation options
        Cv2.Resize(image, output, new Size(sizeX, sizeY), interpolation: InterpolationFlags.Cubic);

        Outputs[0].Set(output);
        return Result.Ok();
    }
    private ValidationResult ValidateInputs()
    {
        var validator = new ResizeValidator(Inputs[0].Get<Mat>());
        return validator.Validate(Inputs);
    }

}

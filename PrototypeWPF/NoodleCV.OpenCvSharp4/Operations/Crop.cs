using FluentValidation.Results;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Crop : IOperation
{
    public List<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create(0),
        OperationInput.Create(0),
        OperationInput.Create(50),
        OperationInput.Create(50),
    };

    public List<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public Result Execute()
    {
        var startX = Inputs[1].Get<int>();
        var startY = Inputs[2].Get<int>();
        var width = Inputs[3].Get<int>();
        var height = Inputs[4].Get<int>();

        ValidationResult validationResult = ValidateInputs();
        if (!validationResult.IsValid)
        {
            return Result.Error(validationResult.Errors);
        }

        var output = new Mat(Inputs[0].Get<Mat>(), new Rect(startX, startY, width, height));

        Outputs[0].Set(output);
        return Result.Ok();
    }

    private ValidationResult ValidateInputs()
    {
        var validator = new CropValidator(Inputs[0].Get<Mat>());
        return validator.Validate(Inputs);
    }

}


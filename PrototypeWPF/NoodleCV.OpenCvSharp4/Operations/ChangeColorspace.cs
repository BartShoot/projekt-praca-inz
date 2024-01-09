using FluentValidation.Results;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;
public class ChangeColorspace : IOperation
{
    public List<OperationInput> Inputs { get; } = new List<OperationInput>()
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create(ColorConversionCodes.BGR2GRAY)
    };

    public List<OperationOutput> Outputs { get; } = new List<OperationOutput>()
    {
        OperationOutput.Create<Mat>(),
    };

    public Result Execute()
    {
        var conversionCodes = Inputs[1].Get<ColorConversionCodes>();

        ValidationResult result = ValidateInputs();
        if (!result.IsValid)
        {
            return Result.Error(result.Errors);
        }

        var output = Inputs[0].Get<Mat>().CvtColor(conversionCodes);
        Outputs[0].Set(output);
        return Result.Ok();
    }

    private ValidationResult ValidateInputs()
    {
        var validator = new ChangeColorspaceValidator();
        return validator.Validate(Inputs);
    }
}
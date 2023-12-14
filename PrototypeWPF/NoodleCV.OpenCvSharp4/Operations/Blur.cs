﻿using FluentValidation.Results;
using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Blur : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create(7),
        OperationInput.Create(5)
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public Result Execute()
    {
        var image = Inputs[0].Get<Mat>();
        var size = Inputs[1].Get<int>();
        var strength = Inputs[2].Get<double>();

        var output = new Mat();

        ValidationResult validationResult = ValidateInputs();
        if (!validationResult.IsValid)
        {
            return new Result(validationResult.Errors);
        }

        Cv2.GaussianBlur(image, output, new Size(size, size), strength);

        Outputs[0].Set(output);
        return new Result();
    }

    private ValidationResult ValidateInputs()
    {
        var validator = new BlurValidator();
        return validator.Validate(Inputs);
    }
}
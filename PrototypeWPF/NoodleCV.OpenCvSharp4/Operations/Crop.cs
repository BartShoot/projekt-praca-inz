﻿using OpenCvSharp;

namespace NoodleCV.OpenCvSharp4.Operations;

public class Crop : IOperation
{
    public IReadOnlyList<OperationInput> Inputs { get; } = new List<OperationInput>
    {
        OperationInput.Create<Mat>(),
        OperationInput.Create<int>(),
        OperationInput.Create<int>(),
        OperationInput.Create<int>(),
        OperationInput.Create<int>(),
    };

    public IReadOnlyList<OperationOutput> Outputs { get; } = new List<OperationOutput>
    {
        OperationOutput.Create<Mat>()
    };

    public void Execute()
    {
        var startX = Inputs[1].Get<int>();
        var startY = Inputs[2].Get<int>();
        var width = Inputs[3].Get<int>();
        var height = Inputs[4].Get<int>();

        var output = new Mat(Inputs[0].Get<Mat>(), new Rect(startX, startY, width, height));

        Outputs[0].Set(output);
    }
}

﻿using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels.Operations;

public class SaveImageViewModel : ViewModelBase, IOperationViewModel
{
    public SaveImageViewModel()
    {
        Operation = new SaveImage();
        NodeInput = new List<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new List<OperationData>
        {
            Operation.Inputs[1]
        };
    }
    private string _imagePath;

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _imagePath, value);
        }
    }

    public string Name => "Save image";

    public IOperation Operation { get; }

    public IReadOnlyList<OperationData> NodeInput { get; }

    public IReadOnlyList<OperationData> NodeParameter { get; }
}
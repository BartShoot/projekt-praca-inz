﻿using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using OpenCvSharp;
using System;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class ChangeColorspaceViewModel : OperationViewModel
{
    public ChangeColorspaceViewModel()
    {
        Operation = new ChangeColorspace();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1]
        };
    }
    private ColorConversionCodes _conversionCodes;

    public ColorConversionCodes ConversionCodes
    {
        get => _conversionCodes;
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _conversionCodes, value);
        }
    }
    public Array ColorCodesList { get => Enum.GetValues(typeof(ColorConversionCodes)); }
}

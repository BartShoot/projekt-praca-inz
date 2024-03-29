﻿using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public class CropViewModel : OperationViewModel, IOperationViewModel
{
    public CropViewModel()
    {
        Operation = new Crop();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1],
            Operation.Inputs[2],
            Operation.Inputs[3],
            Operation.Inputs[4]
        };
    }
    private int _startX;
    public int StartX
    {
        get => Operation.Inputs[1].Get<int>();
        set
        {
            Operation.Inputs[1].Set(value);
            SetProperty(ref _startX, value);
        }
    }

    private int _startY;
    public int StartY
    {
        get => Operation.Inputs[2].Get<int>();
        set
        {
            Operation.Inputs[2].Set(value);
            SetProperty(ref _startY, value);
        }
    }

    private int _width;
    public int Width
    {
        get => Operation.Inputs[3].Get<int>();
        set
        {
            Operation.Inputs[3].Set(value);
            SetProperty(ref _width, value);
        }
    }

    private int _height;
    public int Height
    {
        get => Operation.Inputs[4].Get<int>();
        set
        {
            Operation.Inputs[4].Set(value);
            SetProperty(ref _height, value);
        }
    }

    public void CopyParameter(IOperationViewModel operationViewModel)
    {
        var vm = operationViewModel as CropViewModel;
        Height = vm.Height;
        Width = vm.Width;
        StartX = vm.StartX;
        StartY = vm.StartY;
    }
}

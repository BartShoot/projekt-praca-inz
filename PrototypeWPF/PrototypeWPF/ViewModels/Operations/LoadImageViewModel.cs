using System.Collections.Generic;
using OpenCvSharp;
using NoodleCV;
using Microsoft.Win32;
using NoodleCV.OpenCvSharp4.Operations;

namespace PrototypeWPF.ViewModels.Operations;

public class LoadImageViewModel: ViewModelBase, IOperationViewModel
{
    public LoadImageViewModel()
    {
        Operation = new LoadImage();
        NodeInput = new List<OperationData>
        {
        };
        NodeParameter = new List<OperationData>
        {
            Operation.Inputs[0]
        };
    }
    
    private string _imagePath;
    public string ImagePath
    {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    public string Name => "Load image";
    public IOperation Operation { get; } = new LoadImage();
    public IReadOnlyList<OperationData> NodeInput { get; }
    public IReadOnlyList<OperationData> NodeParameter { get; }

    public void LoadImage()
    {
        var op = new OpenFileDialog();
        op.Title = "Select a picture";
        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";

        if (op.ShowDialog() == true)
        {
            ImagePath = op.FileName;
            Operation.Inputs[0].Set(ImagePath);
            
            Operation.Execute();
        }
    }
}
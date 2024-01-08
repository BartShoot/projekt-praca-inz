using Microsoft.Win32;
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using PrototypeWPF.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PrototypeWPF.ViewModels.Operations;

public class LoadImageViewModel : OperationViewModel
{
    public LoadImageViewModel()
    {
        Operation = new LoadImage();
        NodeInput = new ObservableCollection<OperationData>
        {
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        LoadImageCommand = new DelegateCommand(LoadImage);
    }

    private string _imagePath;
    public string ImagePath
    {
        get => _imagePath;
        set
        {
            Operation.Inputs[0].Set(value);
            SetProperty(ref _imagePath, value);
        }
    }

    public ICommand LoadImageCommand { get; }

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
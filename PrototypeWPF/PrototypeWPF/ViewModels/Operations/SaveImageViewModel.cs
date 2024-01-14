using Microsoft.Win32;
using NoodleCV;
using NoodleCV.OpenCvSharp4.Operations;
using PrototypeWPF.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PrototypeWPF.ViewModels.Operations;

public class SaveImageViewModel : OperationViewModel, IOperationViewModel
{
    public SaveImageViewModel()
    {
        Operation = new SaveImage();
        NodeInput = new ObservableCollection<OperationData>
        {
            Operation.Inputs[0]
        };
        NodeParameter = new ObservableCollection<OperationData>
        {
            Operation.Inputs[1]
        };
        SaveImageCommand = new DelegateCommand(SaveImage);
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

    public ICommand SaveImageCommand { get; }

    public void SaveImage()
    {
        var op = new SaveFileDialog();
        op.Title = "Save image";
        op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                    "Portable Network Graphic (*.png)|*.png";

        if (op.ShowDialog() == true)
        {
            ImagePath = op.FileName;

            Operation.Execute();
        }
    }

    public void CopyParameter(IOperationViewModel operationViewModel)
    {
        var vm = operationViewModel as SaveImageViewModel;
        ImagePath = vm.ImagePath;
    }
}

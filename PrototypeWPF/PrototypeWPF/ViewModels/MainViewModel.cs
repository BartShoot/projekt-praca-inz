using PrototypeWPF.Model;
using PrototypeWPF.ViewModels.Editor;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public IReadOnlyList<OperationDescriptor> AllOperations { get; } = new List<OperationDescriptor>
        {
            new OperationDescriptor("Load image", () => new LoadImageViewModel()),
            new OperationDescriptor("Blur", () => new BlurViewModel()),
            new OperationDescriptor("Change Colorspace", () => new ChangeColorspaceViewModel()),
            new OperationDescriptor("Crop", () => new CropViewModel()),
            new OperationDescriptor("Edge detect", () => new EdgeDetectViewModel()),
            new OperationDescriptor("Save image", () => new SaveImageViewModel()),
            new OperationDescriptor("Resize", () => new ResizeViewModel()),
        };

    public EditorViewModel EditorViewModel { get; }
    public MainViewModel()
    {
        EditorViewModel = new EditorViewModel(AllOperations);
    }
}
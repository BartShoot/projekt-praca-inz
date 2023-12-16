using PrototypeWPF.ViewModels.Editor;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public IReadOnlyList<OperationViewModel> AllOperations { get; } = new List<OperationViewModel>
        {
            new LoadImageViewModel(),
            new BlurViewModel(),
            new ResizeViewModel(),
            new ChangeColorspaceViewModel(),
            new CropViewModel(),
            new EdgeDetectViewModel(),
            new SaveImageViewModel(),
        };

    public EditorViewModel EditorViewModel { get; }
    public MainViewModel()
    {
        EditorViewModel = new EditorViewModel(AllOperations);
    }
}
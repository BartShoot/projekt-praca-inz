using PrototypeWPF.ViewModels.Editor;
using PrototypeWPF.ViewModels.Operations;
using System.Collections.Generic;

namespace PrototypeWPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public IReadOnlyList<IOperationViewModel> AllOperations { get; } = new List<IOperationViewModel>
        {
            new LoadImageViewModel(),
            new BlurViewModel(),
            new ResizeViewModel(),
            new ChangeColorspaceViewModel(),
            new CropViewModel(),
            new EdgeDetectViewModel(),
            new SaveImageViewModel(),
        };

    public EditorViewModel EditorViewModel { get; } = new EditorViewModel();
    public MainViewModel()
    {
        EditorViewModel.InitializeMenu(AllOperations);
    }
}
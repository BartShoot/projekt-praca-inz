using System.Collections.Generic;
using PrototypeWPF.ViewModels.Operations;

namespace PrototypeWPF.ViewModels;

public class MainViewModel: ViewModelBase
{
    public IReadOnlyList<IOperationViewModel> AllOperations { get; } = new List<IOperationViewModel>
        {
            new LoadImageViewModel(),
            new BlurViewModel(),
            new ResizeViewModel()
        };

    public EditorViewModel EditorViewModel { get; } = new EditorViewModel();
}
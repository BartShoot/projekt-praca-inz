using NoodleCV;
using System.Collections.ObjectModel;

namespace PrototypeWPF.ViewModels.Operations;

public abstract class OperationViewModel : ViewModelBase
{
    private ObservableCollection<OperationData> _nodeInput;
    private ObservableCollection<OperationData> _nodeParameter;
    public IOperation Operation { get; protected set; }
    public ObservableCollection<OperationData> NodeInput
    {
        get => _nodeInput;
        protected set => SetProperty(ref _nodeInput, value);
    }
    public ObservableCollection<OperationData> NodeParameter { get => _nodeParameter; protected set => SetProperty(ref _nodeParameter, value); }
    public void Execute()
        => Operation.Execute();
}
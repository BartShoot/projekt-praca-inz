using NoodleCV;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

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

    public ObservableCollection<OperationData> NodeParameter
    {
        get => _nodeParameter; 
        protected set => SetProperty(ref _nodeParameter, value);
    }
    public void Execute()
        => Operation.Execute();
    
    protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        Execute();
        RaisePropertyChanged(nameof(Operation));
        return base.SetProperty(ref storage, value, propertyName);
    }
}
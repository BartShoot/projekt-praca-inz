using NoodleCV;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PrototypeWPF.ViewModels.Operations
{
    public interface IOperationViewModel : INotifyPropertyChanged
    {
        ObservableCollection<OperationData> NodeInput { get; }
        ObservableCollection<OperationData> NodeParameter { get; }
        IOperation Operation { get; }

        void CopyParameter(IOperationViewModel operationViewModel);
        Result Execute();
    }
}
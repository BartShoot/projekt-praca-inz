using System.Collections.Generic;
using NoodleCV;

namespace PrototypeWPF.ViewModels.Operations;

public abstract class OperationViewModel: ViewModelBase
{
    public string Name { get; protected set; }
    public IOperation Operation { get; protected set; }
    public IReadOnlyList<OperationData> NodeInput { get; protected set; }
    public IReadOnlyList<OperationData> NodeParameter { get; protected set; }
    public void Execute() 
        => Operation.Execute();
}
using System.Collections.Generic;
using NoodleCV;

namespace PrototypeWPF.ViewModels.Operations;

public interface IOperationViewModel 
{
    public string Name { get; }
    public IOperation Operation { get; }
    public IReadOnlyList<OperationData> NodeInput { get; }
    public IReadOnlyList<OperationData> NodeParameter { get; }
    public void Execute() 
        => Operation.Execute();
}
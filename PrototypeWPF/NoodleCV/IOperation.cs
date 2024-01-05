namespace NoodleCV
{
    public interface IOperation
    {
        List<OperationInput> Inputs { get; }
        List<OperationOutput> Outputs { get; }
        Result Execute();
    }
}
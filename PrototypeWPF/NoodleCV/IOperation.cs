﻿namespace NoodleCV
{
    public interface IOperation
    {
        IReadOnlyList<OperationInput> Inputs { get; }
        IReadOnlyList<OperationOutput> Outputs { get; }
        void Execute();
    }
}
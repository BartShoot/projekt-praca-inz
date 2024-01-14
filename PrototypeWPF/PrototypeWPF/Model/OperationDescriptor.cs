using PrototypeWPF.ViewModels.Operations;
using System;

namespace PrototypeWPF.Model
{
    public class OperationDescriptor
    {
        private readonly Func<IOperationViewModel> _createViewModel;

        public OperationDescriptor(string name, Func<IOperationViewModel> createViewModel)
        {
            Name = name;
            _createViewModel = createViewModel;
        }

        public string Name { get; }

        public IOperationViewModel CreateViewModel()
            => _createViewModel();
    }
}

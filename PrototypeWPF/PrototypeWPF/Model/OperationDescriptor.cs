using System;
using PrototypeWPF.ViewModels.Operations;

namespace PrototypeWPF.Model
{
    public class OperationDescriptor
    {
        private readonly Func<OperationViewModel> _createViewModel;

        public OperationDescriptor(string name, Func<OperationViewModel> createViewModel)
        {
            Name = name;
            _createViewModel = createViewModel;
        }

        public string Name { get; }

        public OperationViewModel CreateViewModel()
            => _createViewModel();
    }
}

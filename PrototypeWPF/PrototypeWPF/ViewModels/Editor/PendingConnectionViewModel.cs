using PrototypeWPF.Utilities;
using System.Windows.Input;

namespace PrototypeWPF.ViewModels.Editor;

public class PendingConnectionViewModel
{
    private readonly EditorViewModel _editor;
    private ConnectorViewModel _source;

    public PendingConnectionViewModel(EditorViewModel editor)
    {
        _editor = editor;
        StartCommand = new DelegateCommand<ConnectorViewModel>(source => _source = source);
        FinishCommnd = new DelegateCommand<ConnectorViewModel>(target =>
        {
            if (target != null)
            {
                _editor.Connect(_source, target);
            }
        });
    }

    public ICommand StartCommand { get; }
    public ICommand FinishCommnd { get; }
}

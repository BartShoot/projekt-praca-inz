using NoodleCV;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

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
    public Result Execute()
        => Operation.Execute();
    public OperationViewModel()
    {
    }

    protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        var result = Execute();
        RaisePropertyChanged(nameof(Operation));

        var mainWindow = Application.Current.MainWindow as MainWindow;
        var editor = mainWindow.EditorViewName;

        if (result.Status.Equals(true) && editor.RootSnackbar.IsShown)
        {
            editor.RootSnackbar.Hide();
        }
        if (result.Status.Equals(false))
        {
            string errors = string.Join(", ", result.Errors);
            errors += '.';
            (editor)?.RootSnackbar.Show("Can't calculate " +
                Regex.Replace(Operation.GetType().Name, "(\\B[A-Z])", " $1"), errors,
                Wpf.Ui.Common.SymbolRegular.TextBulletListSquareWarning16,
                Wpf.Ui.Common.ControlAppearance.Danger);
        }
        return base.SetProperty(ref storage, value, propertyName);
    }
}
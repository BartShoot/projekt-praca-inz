using Avalonia.Interactivity;
using Avalonia.Controls;

namespace Prototype.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		string _selectedFile;

		public string SelectedFile { get => _selectedFile; set => _selectedFile = value; }
	}
}
using Avalonia.Controls;
using NoodleCV.Views;

namespace NoodleCV.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public string Greeting => "Welcome to Avalonia!";
		public UserControl NodeCanvas = new NodeCanvas();
	}
}
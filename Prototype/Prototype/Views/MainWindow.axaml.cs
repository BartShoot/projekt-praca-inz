using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using OpenCvSharp;
using Prototype.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Window = Avalonia.Controls.Window;

namespace Prototype.Views
{
	public partial class MainWindow : Window
	{
		MainWindowViewModel MainWindowViewModel = new MainWindowViewModel();
		public MainWindow()
		{
			InitializeComponent();
		}

		[RelayCommand]
		public void SelectFileDialog()
		{
			var dialog = new OpenFileDialog();
			var filename = dialog.ShowAsync(this).Result.FirstOrDefault();
			MainWindowViewModel.SelectedFile = filename;
		}
	}
}
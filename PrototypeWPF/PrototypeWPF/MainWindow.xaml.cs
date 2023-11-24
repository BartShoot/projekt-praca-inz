using OpenCvSharp;
using PrototypeWPF.Operations;
using PrototypeWPF.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PrototypeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private List<IOperation> _operations = new();
        private List<IOperation> _allOperations = new();
        public string path;
        Mat _image = new Mat();
        Mat _backup = new Mat();

        public MainWindow()
        {
            this.Closing += WindowClosingEventHandler;

            _allOperations.Add(new SelectImage());
            _allOperations.Add(new Blur());
            _allOperations.Add(new ChangeColorspace());
            _allOperations.Add(new SaveImage());
            _allOperations.Add(new EdgeDetect());
            _allOperations.Add(new Dilation());
            _allOperations.Add(new Resize());
            _allOperations.Add(new Crop());

            InitializeComponent();

            for (int i = 0; i < _allOperations.Count; i++)
            {
                OperationList.Items.Add(_allOperations[i].Name);
            }
        }

        private void ExecuteOperations(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var operation in _operations)
            {
                operation.Input = _image;
                _image = operation.GetFunc();
            }
        }

        private void AddOperation(object sender, System.Windows.RoutedEventArgs e)
        {
            if (OperationList.SelectedIndex >= 0)
            {
                _operations.Add(_allOperations[OperationList.SelectedIndex]);
                PickedOperations.Items.Add((_allOperations[OperationList.SelectedIndex].Name));
            }
        }

        public Bitmap MatToBitmap(Mat image)
        {
            if (image == null)
            {
                return (Bitmap)Bitmap.FromFile("Resources/test.jpg");
            }
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        private void WindowClosingEventHandler(object sender, CancelEventArgs e)
        {
            CancelEventArgs closedEventArgs = e;
            _image.Dispose();
            _backup.Dispose();
            foreach (var operation in _operations)
            {
                operation.Input.Dispose();
                operation.Output.Dispose();
            }
        }

        private void OperationList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: Create new object and save with parameters set up in parameters view
            EditOperation.Content = _allOperations[OperationList.SelectedIndex].ParametersView;
        }

        private void PickedOperations_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // TODO: execute if Output null

            //if (_operations[PickedOperations.SelectedIndex].Output == null)
            //{
            if (PickedOperations.SelectedIndex == 0)
            {
                _operations[PickedOperations.SelectedIndex].GetFunc();
            }
            else
            {
                _operations[PickedOperations.SelectedIndex].Input = _operations[PickedOperations.SelectedIndex - 1].Output;
                _operations[PickedOperations.SelectedIndex].GetFunc();
            }
            //}
            var input = _operations[PickedOperations.SelectedIndex].Input;
            var output = _operations[PickedOperations.SelectedIndex].Output;
            //imageDisplay.Source = BitmapExtensions.ToBitmapSourceBGR(MatToBitmap(input));
            //imageProcessed.Source = BitmapExtensions.ToBitmapSourceBGR(MatToBitmap(output));
            EditOperation.Content = _operations[PickedOperations.SelectedIndex].ParametersView;
        }
    }
}
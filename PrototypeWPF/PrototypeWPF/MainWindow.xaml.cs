using Microsoft.Win32;
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

        public List<IOperation> operations = new();
        public List<IOperation> allOperations = new();
        public string path;
        Mat image = new Mat();
        Mat backup = new Mat();

        public MainWindow()
        {
            this.Closing += WindowClosingEventHandler;

            allOperations.Add(new Blur());
            allOperations.Add(new ColorToGrayscale());
            allOperations.Add(new SaveImage());

            InitializeComponent();
            for (int i = 0; i < allOperations.Count; i++)
            {
                OperationList.Items.Add(allOperations[i].Name);
            }
        }

        private void ExecuteOperations(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var operation in operations)
            {
                operation.Image = image;
                image = operation.GetFunc();
            }
            var placeholder2 = MatToBitmap(image);
            imageProcessed.Source = placeholder2.ToBitmapSourceGrayscale();
            image = backup;
        }

        private void AddOperation(object sender, System.Windows.RoutedEventArgs e)
        {
            operations.Add(allOperations[OperationList.SelectedIndex]);
            PickedOperations.Items.Add((allOperations[OperationList.SelectedIndex].Name));
        }
        public Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }
        private void WindowClosingEventHandler(object sender, CancelEventArgs e)
        {
            CancelEventArgs closedEventArgs = e;
            image.Dispose();
        }

        private void PickImage(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image = new Mat(op.FileName, ImreadModes.Color);
                backup = image;
                var placeholder = MatToBitmap(image);
                imageDisplay.Source = placeholder.ToBitmapSourceBGR();
            }
        }

        private void OperationList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditOperation.Content = allOperations[OperationList.SelectedIndex].ParametersView;
        }

        private void PickedOperations_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditOperation.Content = operations[PickedOperations.SelectedIndex].ParametersView;
        }
    }
}

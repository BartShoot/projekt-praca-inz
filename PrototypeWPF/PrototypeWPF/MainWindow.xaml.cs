﻿using Microsoft.Win32;
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

            _allOperations.Add(new Blur());
            _allOperations.Add(new ChangeColorspace());
            _allOperations.Add(new SaveImage());

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
                operation.Image = _image;
                _image = operation.GetFunc();
            }

            var placeholder2 = MatToBitmap(_image);
            imageProcessed.Source = placeholder2.ToBitmapSourceGrayscale();
            _image = _backup;
        }

        private void AddOperation(object sender, System.Windows.RoutedEventArgs e)
        {
            _operations.Add(_allOperations[OperationList.SelectedIndex]);
            PickedOperations.Items.Add((_allOperations[OperationList.SelectedIndex].Name));
        }

        public Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        private void WindowClosingEventHandler(object sender, CancelEventArgs e)
        {
            CancelEventArgs closedEventArgs = e;
            _image.Dispose();
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
                _image = new Mat(op.FileName, ImreadModes.Color);
                _backup = _image;
                var placeholder = MatToBitmap(_image);
                imageDisplay.Source = placeholder.ToBitmapSourceBGR();
            }
        }

        private void OperationList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditOperation.Content = _allOperations[OperationList.SelectedIndex].ParametersView;
        }

        private void PickedOperations_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EditOperation.Content = _operations[PickedOperations.SelectedIndex].ParametersView;
        }
    }
}
using Microsoft.Win32;
using OpenCvSharp;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    internal class SaveImage : IOperation
    {
        private Mat _input;
        private Mat _output;
        private string _path;

        public Mat Input
        {
            get { return _input; }
            set => _input = value;
        }

        public Mat Output
        {
            get { return _output; }
            set => _output = value;
        }

        public string Name => "Save";

        public string Description => "Saves the image";

        public Func<Mat> GetFunc => () =>
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                _path = saveFileDialog1.FileName;
            }

            Input.SaveImage(_path);
            Output = Input;
            return Output;
        };

        public UserControl ParametersView => new UserControl();
    }
}
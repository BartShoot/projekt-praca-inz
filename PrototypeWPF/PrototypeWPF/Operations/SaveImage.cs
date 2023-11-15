using Microsoft.Win32;
using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class SaveImage : IOperation
    {
        private Mat _input;
        private Mat _output;
        private string _path;

        public string Path
        {
            get { return _path; }
            set => _path = value;
        }

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
            if (_path == null && saveFileDialog1.ShowDialog() == true)
            {
                _path = saveFileDialog1.FileName;
            }
            Input.SaveImage(_path);
            Output = Input;
            return Output;
        };

        public UserControl ParametersView => new SaveImageView(this);
    }
}
using Microsoft.Win32;
using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class SelectImage : IOperation
    {
        private Mat _input;
        private Mat _output;
        private string _path;

        public string Name => "Select image";

        public string Description => "Select image from disk";

        public Func<Mat> GetFunc => () =>
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (Path != null)
            {
                _input = new Mat(Path, ImreadModes.Unchanged);
            }
            else if (op.ShowDialog() == true)
            {
                Path = op.FileName;
                _input = new Mat(op.FileName, ImreadModes.Unchanged);
            }
            Output = Input;
            return Output;
        };

        public UserControl ParametersView => new SelectImageView(this);

        public string Path { get => _path; set => _path = value; }
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

    }
}

using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class Resize : IOperation
    {
        private Mat _input;
        private Mat _output;
        private int _sizeX;
        private int _sizeY;

        public int SizeY
        {
            get { return _sizeY; }
            set { _sizeY = value; }
        }


        public int SizeX
        {
            get { return _sizeX; }
            set { _sizeX = value; }
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

        public string Name => "Resize";

        public string Description => "Resize the image to new dimensions";

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            Output = Output.Resize(new Size(_sizeX, _sizeY), interpolation: InterpolationFlags.Cubic);
            return Output;
        };

        public UserControl ParametersView => new ResizeView(this);
    }
}

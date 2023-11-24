using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class Blur : IOperation
    {
        private Mat _input;

        private Mat _output;
        private int _size = 4;

        public int Size
        {
            get => _size;
            set => _size = value;
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

        public string Name => "Blur";

        public string Description => "Blur the image";

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            Output.Blur(new OpenCvSharp.Size(_size, _size));
            return Output;
        };

        public UserControl ParametersView => new BlurView(this);
    }
}
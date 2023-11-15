using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class Dilation : IOperation
    {
        private Mat _input;
        private Mat _output;
        private int _iterations = 3;
        private double _size = 7;

        public double Size
        {
            get { return _size; }
            set { _size = value; }
        }


        public int Iterations
        {
            get { return _iterations; }
            set { _iterations = value; }
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
        public string Name => "Image dilation";

        public string Description => "Dilate edges from cascaded image to enhance edge detectablity";

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            Output = Output.Dilate(new Mat(new Size(_size, _size), MatType.CV_8U), iterations: _iterations);
            return Output;
        };

        public UserControl ParametersView => new DilationView(this);
    }
}

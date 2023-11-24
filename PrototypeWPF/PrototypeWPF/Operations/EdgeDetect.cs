using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class EdgeDetect : IOperation
    {
        private Mat _input;
        private Mat _output;
        private double _threshold1 = 100;
        private double _threshold2 = 127;

        public double Threshold2
        {
            get { return _threshold2; }
            set { _threshold2 = value; }
        }

        public double Threshold1
        {
            get { return _threshold1; }
            set { _threshold1 = value; }
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

        public string Name => "Edge detect";

        public string Description => "Detect edges using Canny method";

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            Output = Output.Canny(_threshold1, _threshold2);
            return Output;
        };

        public UserControl ParametersView => new EdgeDetectView(this);
    }
}
using System;
using System.Windows.Controls;
using OpenCvSharp;
using PrototypeWPF.OperationsViews;

namespace PrototypeWPF.Operations
{
    public class ChangeColorspace : IOperation
    {
        private Mat _input;
        private Mat _output;

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

        public ColorConversionCodes ConversionCodes
        {
            get => _conversionCodes;
            set => _conversionCodes = value;
        }

        public string Name => "Change colorspace";

        public string Description => "Transform from current colorspace to another";

        private ColorConversionCodes _conversionCodes;

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            Output.CvtColor(_conversionCodes);
            return Output;
        };

        public UserControl ParametersView => new ChangeColorspaceView(this);
    }
}
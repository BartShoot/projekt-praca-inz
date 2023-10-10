using System;
using System.Windows.Controls;
using OpenCvSharp;
using PrototypeWPF.OperationsViews;

namespace PrototypeWPF.Operations
{
    public class ChangeColorspace : IOperation
    {
        private Mat _image;

        public Mat Image
        {
            get { return _image; }
            set => _image = value;
        }

        public ColorConversionCodes ConversionCodes
        {
            get => _conversionCodes;
            set => _conversionCodes = value;
        }

        public string Name => "Change colorspace";

        public string Description => "Transform from current colorspace to another";

        private ColorConversionCodes _conversionCodes;

        public Func<Mat> GetFunc => () => Image.CvtColor(_conversionCodes);

        public UserControl ParametersView => new ChangeColorspaceView(this);
    }
}
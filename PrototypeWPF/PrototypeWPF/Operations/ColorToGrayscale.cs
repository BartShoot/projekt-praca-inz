using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class ColorToGrayscale : IOperation
    {
        private Mat _image;

        public Mat Image
        {
            get { return _image; }
            set => _image = value;
        }

        public string Name => "Color to Grayscale";

        public string Description => "Transform the colorspace to Grayscale";

        private ColorConversionCodes _conversionCodes;

        public Func<Mat> GetFunc => () => Image.CvtColor(_conversionCodes);

        public UserControl ParametersView => new ColorToGrayscaleView(this);

        public ColorConversionCodes ConversionCodes
        {
            get => _conversionCodes;
            set => _conversionCodes = value;
        }
    }
}
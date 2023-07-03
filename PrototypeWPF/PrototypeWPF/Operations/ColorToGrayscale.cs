using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeWPF.Operations
{
    internal class ColorToGrayscale : IOperation
    {
        private Mat _image;
        public Mat Image { get { return _image; } set => _image = value; }

        public string Name => "Color to Grayscale";

        public string Description => "Transform the colorspace to Grayscale";

        public Func<Mat> GetFunc => () => Image.CvtColor(ColorConversionCodes.RGB2GRAY);
    }
}

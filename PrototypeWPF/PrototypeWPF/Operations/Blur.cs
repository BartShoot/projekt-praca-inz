using OpenCvSharp;
using System;

namespace PrototypeWPF.Operations
{
    internal class Blur : IOperation
    {
        public string Name => "Blur";

        public string Description => "Blur the image";

        public Func<Mat> GetFunc => () => Image.Blur(new OpenCvSharp.Size(16, 16));
        private Mat _image;
        public Mat Image { get { return _image; } set => _image = value; }
    }
}

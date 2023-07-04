using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class Blur : IOperation
    {
        public string Name => "Blur";

        public string Description => "Blur the image";

        public Func<Mat> GetFunc => () => Image.Blur(new OpenCvSharp.Size(_size, _size));
        private int _size = 4;
        public int Size { get => _size; set => _size = value; }
        private Mat _image;

        public Mat Image { get { return _image; } set => _image = value; }

        public UserControl ParametersView => new BlurView(this);
    }
}

using OpenCvSharp;
using PrototypeWPF.OperationsViews;
using System;
using System.Windows.Controls;

namespace PrototypeWPF.Operations
{
    public class Crop : IOperation
    {
        private Mat _input;
        private Mat _output;
        private int _startX;
        private int _startY;
        private int _width;
        private int _height;

        public int ImageHeight
        {
            get { return _height; }
            set { _height = value; }
        }

        public int ImageWidth
        {
            get { return _width; }
            set { _width = value; }
        }

        public int StartY
        {
            get { return _startY; }
            set { _startY = value; }
        }

        public int StartX
        {
            get { return _startX; }
            set { _startX = value; }
        }


        public Mat Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public Mat Input
        {
            get { return _input; }
            set { _input = value; }
        }

        public string Name => "Crop";

        public string Description => "Crop the image to desired dimensions";

        public Func<Mat> GetFunc => () =>
        {
            Output = Input;
            // TODO: Limit crop to not go out of bounds
            Output = new Mat(Output, new Rect(_startX, _startY, _width, _height));
            return Output;
        };

        public UserControl ParametersView => new CropView(this);
    }
}

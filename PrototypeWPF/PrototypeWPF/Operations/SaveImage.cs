using Microsoft.Win32;
using OpenCvSharp;
using System;

namespace PrototypeWPF.Operations
{
    internal class SaveImage : IOperation
    {
        private Mat _image;
        private string _path;
        public Mat Image { get { return _image; } set => _image = value; }

        public string Name => "Save";

        public string Description => "Saves the image";

        public Func<Mat> GetFunc => () =>
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                _path = saveFileDialog1.FileName;
            }
            Image.SaveImage(_path);
            return Image;
        };
    }
}

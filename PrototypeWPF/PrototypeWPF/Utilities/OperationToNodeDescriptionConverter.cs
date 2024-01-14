using NoodleCV;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace PrototypeWPF.Utilities
{
    internal class OperationToNodeDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not IOperation operation)
                return null;

            if (operation.Outputs.Count == 0)
                return null;

            var image = operation.Outputs[0].Get<Mat>();
            if (image is null)
                return "Image not calculated.";

            return @$"Width {image.Width}
Height: {image.Height}
Channels: {image.Channels()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

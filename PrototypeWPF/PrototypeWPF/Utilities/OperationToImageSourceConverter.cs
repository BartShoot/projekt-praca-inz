using OpenCvSharp;
using PrototypeWPF.ViewModels.Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NoodleCV;

namespace PrototypeWPF.Utilities;

internal class OperationToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IOperation operation)
            return null;

        if (operation.Outputs.Count == 0)
            return null;

        var image = operation.Outputs[0].Get<Mat>();
        if (image is null)
            return BitmapSource.Create(5, 5, 72, 72, PixelFormats.Indexed1, new BitmapPalette(new List<Color> { Color.FromRgb(128, 128, 128) }), new byte[5 * 8], 8);
        
        return BitmapExtensions.ToBitmapSourceBGR(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

using OpenCvSharp;
using PrototypeWPF.ViewModels.Editor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PrototypeWPF.Utilities;

internal class NodeViewModelToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }
        if (((NodeViewModel)value).OperationViewModel.Operation.Outputs.Count > 0)
        {
            if (((NodeViewModel)value).OperationViewModel.Operation.Outputs[0].Get<Mat>() == null)
                return BitmapSource.Create(5, 5, 72, 72, PixelFormats.Indexed1, new BitmapPalette(new List<Color> { Color.FromRgb(128, 128, 128) }), new byte[5 * 8], 8);
            var ViewModel = (NodeViewModel)value;
            return BitmapExtensions.ToBitmapSourceBGR(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(ViewModel.OperationViewModel.Operation.Outputs[0].Get<Mat>()));
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

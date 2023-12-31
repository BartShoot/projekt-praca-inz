using OpenCvSharp;
using PrototypeWPF.ViewModels.Editor;
using System;
using System.Globalization;
using System.Windows.Data;

namespace PrototypeWPF.Utilities;

internal class NodeViewModelToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            var ViewModel = (NodeViewModel)value;
            return BitmapExtensions.ToBitmapSourceBGR(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(ViewModel.Output[0].Data.Get<Mat>()));
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

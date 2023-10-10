using OpenCvSharp;
using PrototypeWPF.Operations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    /// <summary>
    /// Interaction logic for ColorToGrayscale.xaml
    /// </summary>
    public partial class ColorToGrayscaleView : UserControl
    {
        ColorToGrayscale test;

        public ColorToGrayscaleView(ColorToGrayscale function)
        {
            test = function;
            InitializeComponent();
            ConversionType.ItemsSource = Enum.GetValues(typeof(ColorConversionCodes));
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
        }
    }
}

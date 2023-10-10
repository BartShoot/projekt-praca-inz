using System;
using System.Windows;
using System.Windows.Controls;
using OpenCvSharp;
using PrototypeWPF.Operations;

namespace PrototypeWPF.OperationsViews
{
    /// <summary>
    /// Interaction logic for ColorToGrayscale.xaml
    /// </summary>
    public partial class ChangeColorspaceView : UserControl
    {
        ChangeColorspace test;

        public ChangeColorspaceView(ChangeColorspace function)
        {
            test = function;
            InitializeComponent();
            ConversionType.ItemsSource = Enum.GetValues(typeof(ColorConversionCodes));
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            test.ConversionCodes = (ColorConversionCodes)ConversionType.SelectedValue;
        }
    }
}
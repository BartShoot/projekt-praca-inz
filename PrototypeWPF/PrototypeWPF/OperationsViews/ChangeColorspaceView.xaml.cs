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
        ChangeColorspace changeColorspace;

        public ChangeColorspaceView(ChangeColorspace function)
        {
            changeColorspace = function;
            InitializeComponent();
            ConversionType.ItemsSource = Enum.GetValues(typeof(ColorConversionCodes));
            if (function.ConversionCodes != null)
            {
                ConversionType.SelectedValue = function.ConversionCodes;
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            changeColorspace.ConversionCodes = (ColorConversionCodes)ConversionType.SelectedValue;
        }
    }
}
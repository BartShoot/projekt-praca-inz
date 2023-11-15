using OpenCvSharp;
using PrototypeWPF.Operations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
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
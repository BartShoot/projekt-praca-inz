using System;
using System.Windows;
using System.Windows.Controls;
using PrototypeWPF.Operations;

namespace PrototypeWPF.OperationsViews
{
    /// <summary>
    /// Interaction logic for BlurView.xaml
    /// </summary>
    public partial class BlurView : UserControl
    {
        Blur test;

        public BlurView(Blur function)
        {
            test = function;
            InitializeComponent();
            BlurSize.Text += function.Size.ToString();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            test.Size = Convert.ToInt16(BlurSize.Text);
        }
    }
}
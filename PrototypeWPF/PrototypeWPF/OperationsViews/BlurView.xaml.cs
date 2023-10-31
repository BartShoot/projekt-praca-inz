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
            // TODO: fix not showing size
            var test2 = function.Size.ToString();
            BlurSize.Text += test2;
            BlurSize.UpdateLayout();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            test.Size = Convert.ToInt16(BlurSize.Text);
        }
    }
}
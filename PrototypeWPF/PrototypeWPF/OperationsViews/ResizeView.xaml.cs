using PrototypeWPF.Operations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    public partial class ResizeView : UserControl
    {
        Resize resize;
        public ResizeView(Resize resize)
        {
            this.resize = resize;
            InitializeComponent();
            SizeX.Text += resize.SizeX.ToString();
            SizeY.Text += resize.SizeY.ToString();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            resize.SizeX = Convert.ToInt32(SizeX.Text);
            resize.SizeY = Convert.ToInt32(SizeY.Text);
        }
    }
}

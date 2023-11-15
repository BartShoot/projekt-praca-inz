using PrototypeWPF.Operations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    public partial class DilationView : UserControl
    {
        Dilation dilation;
        public DilationView(Dilation dilation)
        {
            InitializeComponent();
            this.dilation = dilation;
            Iterations.Text = dilation.Iterations.ToString();
            Size.Text = dilation.Size.ToString();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            dilation.Iterations = Convert.ToInt32(Iterations.Text);
            dilation.Size = Convert.ToDouble(Size.Text);
        }
    }
}

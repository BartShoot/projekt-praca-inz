using PrototypeWPF.Operations;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    public partial class EdgeDetectView : UserControl
    {
        EdgeDetect edgeDetect;
        public EdgeDetectView(EdgeDetect edgeDetect)
        {
            this.edgeDetect = edgeDetect;
            InitializeComponent();
            Threshold1.Text += edgeDetect.Threshold1.ToString();
            Threshold2.Text += edgeDetect.Threshold2.ToString();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            edgeDetect.Threshold1 = Convert.ToDouble(Threshold1.Text);
            edgeDetect.Threshold2 = Convert.ToDouble(Threshold2.Text);
        }
    }
}

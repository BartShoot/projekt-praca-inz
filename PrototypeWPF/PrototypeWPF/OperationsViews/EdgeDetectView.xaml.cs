using PrototypeWPF.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

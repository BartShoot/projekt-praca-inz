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
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
        }
    }
}

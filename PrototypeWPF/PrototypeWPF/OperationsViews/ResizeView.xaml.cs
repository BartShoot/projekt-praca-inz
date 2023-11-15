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

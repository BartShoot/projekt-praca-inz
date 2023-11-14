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
            dilation.Size= Convert.ToDouble(Size.Text);
        }
    }
}

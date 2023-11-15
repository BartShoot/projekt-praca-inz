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
    /// Interaction logic for CropView.xaml
    /// </summary>
    public partial class CropView : UserControl
    {
        Crop crop;

        public CropView(Crop crop)
        {
            this.crop = crop;
            InitializeComponent();
            StartX.Text = crop.StartX.ToString();
            StartY.Text = crop.StartY.ToString();
            CropHeight.Text = crop.ImageHeight.ToString();
            CropWidth.Text = crop.ImageWidth.ToString();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            crop.StartX = Convert.ToInt32(StartX.Text);
            crop.StartY = Convert.ToInt32(StartY.Text);
            crop.ImageHeight = Convert.ToInt32(CropHeight.Text);
            crop.ImageWidth = Convert.ToInt32(CropWidth.Text);
        }

    }
}

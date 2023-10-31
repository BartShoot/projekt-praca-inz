using OpenCvSharp;
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
    public partial class SelectImageView : UserControl
    {
        SelectImage selectImage;
        public SelectImageView(SelectImage function)
        {
            selectImage = function;
            InitializeComponent();
            if(selectImage.Path != null)
            {
                FilePath.Text = selectImage.Path;
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            selectImage.Path = FilePath.Text;
        }
    }
}

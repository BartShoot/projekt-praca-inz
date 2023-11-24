using PrototypeWPF.Operations;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    public partial class SelectImageView : UserControl
    {
        SelectImage selectImage;
        public SelectImageView(SelectImage function)
        {
            selectImage = function;
            InitializeComponent();
            if (selectImage.Path != null)
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

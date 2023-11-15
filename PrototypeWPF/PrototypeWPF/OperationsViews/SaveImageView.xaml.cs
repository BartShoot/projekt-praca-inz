using PrototypeWPF.Operations;
using System.Windows;
using System.Windows.Controls;

namespace PrototypeWPF.OperationsViews
{
    /// <summary>
    /// Interaction logic for SaveImageView.xaml
    /// </summary>
    public partial class SaveImageView : UserControl
    {
        SaveImage saveImage;

        public SaveImageView(SaveImage saveImage)
        {
            this.saveImage = saveImage;
            InitializeComponent();
            if (saveImage.Path != null)
            {
                FilePath.Text = saveImage.Path;
            }

        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            saveImage.Path = FilePath.Text;
        }

    }
}

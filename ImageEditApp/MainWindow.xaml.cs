using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageEditApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? FolderDirectory;
        private string? LogoPathDirectory;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog dialogDirectory = new CommonOpenFileDialog { IsFolderPicker = true })
            {
                FolderDirectory = dialogDirectory.ShowDialog() == CommonFileDialogResult.Ok ?
                                dialogDirectory.FileName : null;
            }
            FolderSelecterButton.Content = FolderDirectory;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (CommonOpenFileDialog dialogDirectory = new CommonOpenFileDialog { })
            {
                LogoPathDirectory = dialogDirectory.ShowDialog() == CommonFileDialogResult.Ok ?
                                dialogDirectory.FileName : null;
            }
            LogoFileButton.Content = LogoPathDirectory;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ImageEditor.ImageLoad(FolderDirectory, LogoPathDirectory, Convert.ToDouble(Percentage.Text), PositionBox.Text);
        }
    }
}
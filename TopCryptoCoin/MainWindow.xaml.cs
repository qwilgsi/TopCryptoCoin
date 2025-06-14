using System.Windows;
using TopCryptoCoin.Views;

namespace TopCryptoCoin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage(MainFrame));
        }
    }
}
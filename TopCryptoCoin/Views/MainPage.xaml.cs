using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TopCryptoCoin.Models;
using TopCryptoCoin.ViewModels;

namespace TopCryptoCoin.Views
{
    public partial class MainPage : Page
    {
        public MainPage(Frame mainFrame)
        {
            InitializeComponent();
            DataContext = new MainViewModel(mainFrame);
        }

        private void ListViewCoins_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            if (viewModel?.SelectedCoin != null)
            {
                var detailPage = new CoinDetailPage(viewModel.SelectedCoin);

                viewModel.MainFrame.Navigate(detailPage);
            }
        }
    }
}

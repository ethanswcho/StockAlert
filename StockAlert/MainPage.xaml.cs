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

namespace StockAlert
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void showScanPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ScanPage());
            string url = "https://www.canadacomputers.com/product_info.php?cPath=43_557_559&item_id=181376";
            WebScraper ws = new WebScraper(url);
        }
    }

    
}

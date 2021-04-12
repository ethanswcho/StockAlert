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
            List<string> wanted = new List<string>();
            wanted.Add("3060");
            wanted.Add("3060TI");
            wanted.Add("3070");
            wanted.Add("3080");
            wanted.Add("3090");
            wanted.Add("Test");
            MEScraper mes = new MEScraper(wanted);
            mes.CheckStock();
        }
    }

    
}

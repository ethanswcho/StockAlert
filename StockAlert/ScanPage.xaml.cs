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
using System.Windows.Threading;

namespace StockAlert
{
    /// <summary>
    /// Interaction logic for ScanPage.xaml
    /// </summary>
    public partial class ScanPage : Page
    {
        public ScanPage(List<string> NVIDIAWanted, List<string> AMDWanted)
        {
            InitializeComponent();
            this.NVIDIAWanted = NVIDIAWanted;
            this.AMDWanted = AMDWanted;

            


            // Build Query to only search for selected models/makers
            
        }

        

        // Asynchronously construct UI Manager
        public async void AsyncInitUIM()
        {
            this.uim = await initUIM();

            async Task<UIManager> initUIM()
            {
                uim = new UIManager(
                    (StackPanel)this.FindName("WebsiteBar"),
                    (StackPanel)this.FindName("NVIDIABar"),
                    (StackPanel)this.FindName("AMDBar"),
                    (TextBlock)this.FindName("LoopText"),
                    (TextBlock)this.FindName("StockText"),
                    NVIDIAWanted,
                    AMDWanted
                );
                return uim;
            }
        }



        public void start()
        {
            QueryBuilder qb = new QueryBuilder(NVIDIAWanted, AMDWanted);
            var Query = qb.BuildAndGetQuery();

            StockChecker sc = new StockChecker(uim);
            sc.CheckStock(Query);
        }

        private void showMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        UIManager uim;
        private List<string> NVIDIAWanted;
        private List<string> AMDWanted;

    }
}

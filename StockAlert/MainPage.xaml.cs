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
using System.Diagnostics;
using System.Windows.Threading;

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

            //List models in descending order for uniformity
            NVIDIAWanted = NVIDIAWanted.OrderByDescending(x => x).ToList();
            AMDWanted = AMDWanted.OrderByDescending(x => x).ToList();

            // Testing module. 
            // Query will include pages where the items are always in stock.
            bool testing = false;
            if (testing)
            {
                NVIDIAWanted.Add("Test1");
                NVIDIAWanted.Add("Test2");
            }
            

            // If selection is valid, then transition into ScanPage
            if (isSelectionValid()) {
                ScanPage sp = new ScanPage(NVIDIAWanted, AMDWanted);
                sp.AsyncInitUIM();
                this.NavigationService.Navigate(sp);

                Task.Delay(3000);
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => sp.start())).Wait();
            }
            // Selection is not valid (selection is null - no items chosen)
            // Error message should be displayed
            else
            {
                DisplayErrorMessage();
            }
            
        }

        //
        private bool isSelectionValid()
        {
            if (this.NVIDIAWanted.Count == 0 && this.AMDWanted.Count == 0)
            {
                return false;
            }
            return true;
        }

        // If selection is not Valid, we need to inform the user.
        // Display an error message to inform that they need to make choice(s).
        private void DisplayErrorMessage()
        {
            MessageBox.Show("Please select at least one product", "STOCK ALERT");
        }



        // When a button is clicked, we need to check if this is the 1st click or 2nd click
        private void ButtonPressed(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            Debug.WriteLine(button.Background);
            string text = (string) button.Content;

            // Clicked NVIDIA model
            if (NVIDIAModels.Contains(text))
            {
                // Currently this model is contained in our wants query
                // This means this is a 2nd click, remove from list and revert button pressed ui
                if(NVIDIAWanted.Contains(text))
                {
                    SecondPress(button);
                    NVIDIAWanted.Remove(text);
                }
                // 1st click
                // Add to wanted list
                else
                {
                    FirstPress(button);
                    NVIDIAWanted.Add(text);
                }
            }
            // Repeat above but this is for AMD
            else if (AMDModels.Contains(text))
            {
                if (AMDWanted.Contains(text))
                {
                    SecondPress(button);
                    AMDWanted.Remove(text);
                }
                else
                {
                    FirstPress(button);
                    AMDWanted.Add(text);
                }
            }
        }

        // When this is the button's first press.
        // Shade it darker to show that it has been pressed
        private void FirstPress(Button button)
        {
            button.Background = Brushes.DarkGray;
        }

        // When this is the button's second press
        // Shade it back to original color to revert its darker look
        private void SecondPress(Button button)
        {
            button.Background = defaultColor;
        }

        // Default color for the buttons
        private Brush defaultColor = (Brush)new BrushConverter().ConvertFrom("#FFF7F7F7");

        // User-selected wanted NVIDIA models
        private List<string> NVIDIAWanted = new List<string>();

        // User-selected wanted AND models
        private List<string> AMDWanted = new List<string>();

        // Supported NVIDIA models
        private List<string> NVIDIAModels = new List<string>()
        {
            "3090",
            "3080",
            "3070",
            "3060TI",
            "3060"
        };
        // Supported AMD models
        private List<string> AMDModels = new List<string>()
        {
            "6900XT",
            "6800XT",
            "6800",
            "6700XT",
            "5700XT"
        };
        
    }

    
}

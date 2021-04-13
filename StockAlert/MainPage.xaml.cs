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
            /*
            wanted.Add("3060");
            wanted.Add("3060TI");
            wanted.Add("3070");
            wanted.Add("3080");
            wanted.Add("3090");
            wanted.Add("Test");
            */
            MEScraper mes = new MEScraper(this.NVIDIAWanted, this.AMDWanted);
            mes.CheckStock();
        }

        //Buttons for adding NVIDIA cards
        private void Clicked3090(object sender, RoutedEventArgs e)
        {
            NVIDIAWanted.Add("3090");
        }
        private void Clicked3080(object sender, RoutedEventArgs e)
        {
            NVIDIAWanted.Add("3080");
        }
        private void Clicked3070(object sender, RoutedEventArgs e)
        {
            NVIDIAWanted.Add("3070");
        }
        private void Clicked3060TI(object sender, RoutedEventArgs e)
        {
            NVIDIAWanted.Add("3060TI");
        }
        private void Clicked3060(object sender, RoutedEventArgs e)
        {
            NVIDIAWanted.Add("3060");
        }

        // Button for adding AMD cards
        private void Clicked6900XT(object sender, RoutedEventArgs e)
        {
            AMDWanted.Add("6900XT");
        }
        private void Clicked6800XT(object sender, RoutedEventArgs e)
        {
            AMDWanted.Add("6800XT");
        }
        private void Clicked6800(object sender, RoutedEventArgs e)
        {
            AMDWanted.Add("6800");
        }
        private void Clicked6700XT(object sender, RoutedEventArgs e)
        {
            AMDWanted.Add("6700XT");
        }
        private void Clicked5700XT(object sender, RoutedEventArgs e)
        {
            AMDWanted.Add("5700XT");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

namespace StockAlert
{
    class UIManager
    {
        public UIManager(StackPanel WebsiteBar, StackPanel NVIDIABar, StackPanel AMDBar,
            TextBlock LoopText, TextBlock StockText, 
            List<string> NVIDIAWanted, List<string> AMDWanted)
        {
            this.WebsiteBar = WebsiteBar;
            this.NVIDIABar = NVIDIABar;
            this.AMDBar = AMDBar;
            this.LoopText = LoopText;
            this.StockText = StockText;
            this.NVIDIAWanted = NVIDIAWanted;
            this.AMDWanted = AMDWanted;

            ApplySelection();
            InitWebsiteDict();
        }

        // Apply User selection, which includes
        //    -Displaying selected elements (models)
        //    -Adding respective TextBlock to Dictionary for faster lookups
        private void ApplySelection()
        {
            this.ModelDict = new Dictionary<string, TextBlock>();
            TextBlock tb;

            foreach(string model in NVIDIAWanted)
            {
                tb = GetTextBlock(model);
                NVIDIABar.Children.Add(tb);
                ModelDict[model] = tb;
            }

            foreach(string model in AMDWanted)
            {
                tb = GetTextBlock(model);
                AMDBar.Children.Add(tb);
                ModelDict[model] = tb;
            }
        }

        // Initialize WebsiteDict for faster lookup 
        private void InitWebsiteDict()
        {
            this.WebsiteDict = new Dictionary<Website, TextBlock>();

            foreach(Website ws in Enum.GetValues(typeof(Website)).Cast<Website>())
            {
                foreach(TextBlock child in WebsiteBar.Children)
                {
                    if(child.Text == ws.ToString())
                    {
                        WebsiteDict[ws] = child;
                    }
                }
            }
        }

        private TextBlock GetTextBlock(string text)
        {
            TextBlock tb = new TextBlock();
            tb.FontSize = FontSize;
            tb.Text = text;
            return tb;
        }

        // Display current website we are scraping by highlighting as green color
        public void UpdateUI(Website ws)
        {
            if(CurrentModel is not null)
            {
                this.WebsiteDict[CurrentWebsite].Foreground = DefaultColor;
            }


            this.WebsiteDict[ws].Foreground = CurrentColor;

            this.CurrentWebsite = ws;
        }

        // Display current model we are scraping by highlighting as green color
        public void UpdateUI(string model)
        {
            if(CurrentModel is not null)
            {
                this.ModelDict[CurrentModel].Foreground = DefaultColor;
            }
            this.ModelDict[model].Foreground = CurrentColor;

            TextBlock tb = this.ModelDict[model];
            tb.InvalidateVisual();
            tb.UpdateLayout();
            this.CurrentModel = model;
        }

        public void UpdateLoopText(int count)
        {
            this.LoopText.Text = "Loop#" + count;
        }

        private StackPanel WebsiteBar;
        private StackPanel NVIDIABar;
        private StackPanel AMDBar;
        private TextBlock LoopText;
        private TextBlock StockText;

        private Dictionary<Website, TextBlock> WebsiteDict;
        private Dictionary<string, TextBlock> ModelDict;

        private List<string> NVIDIAWanted;
        private List<string> AMDWanted;

        private Website CurrentWebsite;
        private string CurrentModel;

        private readonly int FontSize = 30;

        private readonly SolidColorBrush DefaultColor = Brushes.Black;
        private readonly SolidColorBrush CurrentColor = Brushes.Green;

    }

    

}

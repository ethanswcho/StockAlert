using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using HtmlAgilityPack;
using System.Collections;
// Debug.WriteLine();
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;
using System.Windows.Threading;


namespace StockAlert
{
    // Designed to batch scrape Memory Express GPUs
    class MEScraper : WebScraper
    {
        public MEScraper(UIManager uim) : base(uim){
            ws = Website.MemoryExpress;
            linkPrefix = "https://www.memoryexpress.com";
         }
        public override void Scrape(Dictionary<Maker, Dictionary<string, string>> URLDict)
        {

            foreach(var l1 in URLDict)
            {
                // Write Maker
                Debug.WriteLine("   " + l1.Key.ToString());
                foreach(var l2 in l1.Value)
                {
                    // Write Model
                    Debug.WriteLine("       " + l2.Key);
                    string GPUInfo = l1.Key.ToString() + l2.Key.ToString();

                    /*
                    Task.Factory.StartNew(() => this.uim.UpdateUI(l2.Key));
                    Application.Current.Dispatcher.Invoke(
                        new Action(() => this.uim.UpdateUI(l2.Key))
                        );
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerAsync(l2.Value);
                    */

                    Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                    {
                        this.uim.UpdateUI(ws);
                        this.uim.UpdateUI(l2.Key);
                    }), DispatcherPriority.ContextIdle,null);

                    // Scrape the given page for given model ex) RTX 30080
                    ScrapePage(l2.Value, GPUInfo);

                    Debug.WriteLine("");
                }
            }
        }

        /*
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Debug.WriteLine("Starting bgw_DoWork!");
            ScrapePage((string)e.Argument);
        }
        */

        private void ScrapePage(string URL, string GPUInfo)
        {
            // Load the webpage containing current GPU (ex. page of only RTX 3080s)
            
            HtmlDocument doc = web.Load(URL);
            // Select all nodes that contain individual item information
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[@class='c-shca-icon-item']");

            Debug.WriteLine("       # of listed items: " + items.Count);

            foreach (HtmlNode item in items)
            {
                // Link for this item (GPU)
                string link = linkPrefix + (string)item.Descendants("a")
                                            .Select(x => x.GetAttributeValue("href", ""))
                                            .Where(x => x.Contains("/Products/"))
                                            .First();

                Debug.WriteLine("           Checking... " + link);

                HtmlNode stockNode = null;
                // Each out of stock item has a subclass that shows that the item is out of stock.
                try
                {
                    stockNode = item.Descendants("div")
                                            .Where(x => x.GetAttributeValue("class", "") == "c-shca-icon-item__body-inventory")
                                            .First();
                }
                // The stockNode is not there, the item is (probably) in stock.
                catch (InvalidOperationException)
                {
                    Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                    {
                        base.InStock(link, GPUInfo);
                    }), DispatcherPriority.ContextIdle, null);
                    continue;
                }

                // The stockNode could be there, but it may not say "Out of Stock"
                // stockNode says "Out of Stock"
                if (stockNode.Descendants("span").First().InnerText == "Out of Stock")
                {
                    base.NotInStock();
                }

                // The stockNode is there but it does not say out of stock. the item is (probably) in stock.
                else
                {
                    Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                    {
                        base.InStock(link, GPUInfo);
                    }), DispatcherPriority.ContextIdle, null);
                    continue;
                } 
            }
        }
        private string linkPrefix;
    }

}


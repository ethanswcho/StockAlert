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

namespace StockAlert
{
    // Designed to batch scrape Memory Express GPUs
    class MEScraper
    {
        public MEScraper(List<string> NVIDIAWanted, List<string> AMDWanted)
        {
            Debug.WriteLine("Selected options:");
            Debug.WriteLine("   NVIDIA:");
            foreach (string model in NVIDIAWanted)
            {
                Debug.WriteLine("       " + model);
            }
            Debug.WriteLine("\n   AMD:");
            foreach(string model in AMDWanted)
            {
                Debug.WriteLine("       " + model);
            }
            Debug.WriteLine("\n");

            this.initNVIDIALinks();
            this.initNVIDIAQuery(NVIDIAWanted);
            /*
             * , List<string> AMDWanted
            this.initAMDLinks();
            this.initAMDQuery(AMDWanted);
            */
        }

        // Initialize website links for NVIDIA products
        private void initNVIDIALinks()
        {
            this.NVIDIALinks = new Dictionary<string, string>();
            NVIDIALinks.Add("3060", "https://www.memoryexpress.com/Category/VideoCards?FilterID=f1b0a6e4-f41e-5fea-c242-d1bac7b02bf2");
            NVIDIALinks.Add("3060TI", "https://www.memoryexpress.com/Category/VideoCards?FilterID=75668704-944f-8e1a-4cca-036beb9638a8");
            NVIDIALinks.Add("3070", "https://www.memoryexpress.com/Category/VideoCards?FilterID=e3034e65-f2ac-35f1-26eb-277b7a7e9ce9");
            NVIDIALinks.Add("3080", "https://www.memoryexpress.com/Category/VideoCards?FilterID=45788ec3-6bb1-e460-abe6-afa274b9d30e");
            NVIDIALinks.Add("3090", "https://www.memoryexpress.com/Category/VideoCards?FilterID=0faf222f-0400-d211-b926-04fdfc0bfa85");
            // Link for testing. Should return 1 in stock.
            NVIDIALinks.Add("Test", "https://www.memoryexpress.com/Category/VideoCards?FilterID=d9eeb36d-9106-8db7-6cef-725efd164818");
        }

        // Initialize website link queries for NVIDIA products
        private void initNVIDIAQuery(List<string> wanted)
        {
            this.NVIDIAQuery = new OrderedDictionary();
            foreach(string name in wanted)
            {
                this.NVIDIAQuery.Add(name, this.NVIDIALinks[name]);
            }

        }

        // Initialize website links for AMD products
        private void initAMDLinks()
        {
            this.AMDLinks = new Dictionary<string, string>();
        }

        // Initialize website link queries for AMD products
        private void initAMDQuery(List<string> wanted)
        {
            this.AMDQuery = new OrderedDictionary();
            foreach (string name in wanted)
            {
                this.AMDQuery.Add(name, this.AMDLinks[name]);
            }
        }

        public void CheckStock()
        {
            Debug.WriteLine("=====Checking Stock in MEMORY EXPRESS=====");
            foreach (DictionaryEntry item in NVIDIAQuery)
            {
                Debug.WriteLine("Checking stock for... " + item.Key);
                this.ScrapePage((string)item.Value);
                Debug.WriteLine("");
            }
        }

        private void ScrapePage(string URL)
        {
            // Load the webpage containing current GPU (ex. page of only RTX 3080s)
            HtmlAgilityPack.HtmlDocument doc = web.Load(URL);
            // Select all nodes that contain individual item information
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[@class='c-shca-icon-item']");
            foreach (HtmlNode item in items)
            {
                // Link for this item (GPU)
                string link = (string)item.Descendants("a")
                                            .Select(x => x.GetAttributeValue("href", ""))
                                            .Where(x => x.Contains("/Products/"))
                                            .First();

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
                    Debug.WriteLine("!!!Item is in stock! " + link);
                    continue;
                }

                // The stockNode could be there, but it may not say "Out of Stock"
                // stockNode says "Out of Stock"
                if (stockNode.Descendants("span").First().InnerText == "Out of Stock")
                {
                    Debug.WriteLine("Item is not in stock... " + link);
                }

                // The stockNode is there but it does not say out of stock. the item is (probably) in stock.
                else
                {
                    // Item is (potentially) in stock. Output the link!
                    Debug.WriteLine("!!!Item is in stock! " + link);
                }
                
            }
        }

        private HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        private OrderedDictionary NVIDIAQuery;
        private OrderedDictionary AMDQuery;
        private Dictionary<string, string> NVIDIALinks;
        private Dictionary<string, string> AMDLinks;
    }
}

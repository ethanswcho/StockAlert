using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Collections.Generic;
// Debug.WriteLine();
using System.Diagnostics;

/*
namespace StockAlert
{

    class SingleScraper
    {
        public SingleScraper(string URL)
        {
            this.Website = Website.CanadaComputers;
            this.URL = URL;
            this.DoWebScrape();
        }
        // Called upon object initialization. 
        // Scrape the URL and from found field make an Item and set it as this field
        private void DoWebScrape()
        {
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            bool InStock = false;

            switch (this.Website)
            {
                case Website.CanadaComputers:

                    // should be false
                    string url = "https://www.canadacomputers.com/product_info.php?cPath=43_557_559&item_id=181376";
                    // should be true
                    string url2 = "https://www.canadacomputers.com/product_info.php?cPath=43_557_559&item_id=114124";
                    HtmlAgilityPack.HtmlDocument doc = web.Load(url2);
                    var root = doc.DocumentNode.SelectNodes("//div[@class='pi-prod-availability']")[0];
                    List<string> texts = new List<string>();
                    foreach (var node in root.DescendantNodesAndSelf())
                    {
                        if (!node.HasChildNodes)
                        {
                            string text = node.InnerText;
                            if (!string.IsNullOrEmpty(text.Trim()))
                                texts.Add(text.Trim());         
                        }
                    }
                    foreach(string i in texts){
                        Debug.WriteLine(i);
                    }

                    // If it's not BOTH not available online AND in-store, then it's probably in stock (either online or in-store)
                    InStock = !(texts.Contains("Not Available Online") & texts.Contains("In-Store Out Of Stock"));
                    Debug.WriteLine(InStock);
                    break;

                case Website.MemoryExpress:
                    break;

                case Website.NewEgg:
                    break;
            }

            this.InStock = InStock;

            this.Item = new Item(0, Company.NVIDIA, "0");
        }

        // Visit this itemURL and update InStock field
        public void CheckStock()
        {
            // Stub
            this.InStock = false;
        }

        private string text;
        private Website Website;
        private string URL;
        private Item Item { get; set; }
        private bool InStock { get; set; }
    }
}
*/
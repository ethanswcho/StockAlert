using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Specialized;
using HtmlAgilityPack;


namespace StockAlert
{
    class StockChecker
    {
        public StockChecker()
        {
            mes = new MEScraper();
            ccs = new CCScraper();
        }

        public void CheckStock(Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>> Query)
        {
            // Iterate over each Website/Stock pair
            foreach(var l1 in Query)
            {
                Website w = l1.Key;
                // Depending on the website set the correct scraper
                var Scraper = GetScraper(w);
                Debug.WriteLine(w.ToString());
                // Scrape using the website's dedicated scraper
                Scraper.Scrape(l1.Value);
            }
        }

        // Sets the current scraper, depending on the website we are looking for
        private WebScraper GetScraper(Website w)
        {
            switch (w)
            {
                case Website.CanadaComputers:
                    return ccs;
                    break;

                case Website.MemoryExpress:
                    return mes;
                    break;

                default:
                    return new WebScraper();
                    break;
            }
        }

        private MEScraper mes;
        private CCScraper ccs;
    }

    

}

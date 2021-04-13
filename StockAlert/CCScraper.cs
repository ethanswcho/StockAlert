﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Collections;
// Debug.WriteLine();
using System.Diagnostics;

namespace StockAlert
{
    class CCScraper : WebScraper
    {
        public CCScraper()
        {
            ws = Website.CanadaComputers;
        }

        public override void Scrape(Dictionary<Maker, Dictionary<string, string>> URLDict)
        {
            foreach (var l1 in URLDict)
            {
                // Write Maker
                Debug.WriteLine("   " + l1.Key.ToString());
                foreach (var l2 in l1.Value)
                {
                    // Write Model
                    Debug.WriteLine("       " + l2.Key);
                    // Scrape the given page for given model ex) RTX 30080
                    ScrapePage(l2.Value);
                    Debug.WriteLine("");
                }

                Debug.WriteLine("");
            }
        }

        private void ScrapePage(string URL)
        {
            HtmlDocument doc = web.Load(URL);
            HtmlNodeCollection items = doc.DocumentNode.SelectNodes("//div[@class='row mx-0']");

            Debug.WriteLine("       # of listed items: " + items.Count);

            foreach (HtmlNode item in items)
            {
                var stockNode = item.Descendants("a")
                            .Where(x => x.GetAttributeValue("title", "") == "View Product Inventory")
                            .First();

                var lines = stockNode.Descendants("div")
                    .Where(x => x.GetAttributeValue("class", "") == "line-height");

                string link = item.Descendants("a")
                        .Where(x => x.GetAttributeValue("href", "").Contains("https://www.canadacomputers.com/product_info"))
                        .First()
                        .GetAttributeValue("href", "");

                Debug.WriteLine("           Checking... " + link);

                // When a product is out of stock, it will not have any lines in the stockNode.
                // Item is likely stock if there are any lines present
                if (lines.Count() > 0)
                {
                    // Get link for this item
                    
                    base.InStock(link);
                }
                else
                {
                    base.NotInStock();
                }

            }


        }
    }
}

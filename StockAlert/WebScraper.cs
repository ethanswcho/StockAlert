using System;
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
    // Parent class of all scraper classes.
    class WebScraper
    {
        public WebScraper()
        {
            web = new HtmlWeb();
        }
        

        // Scrape the web - the method will be different depending on the website.
        public virtual void Scrape(Dictionary<Maker, Dictionary<string, string>> URLDict)
        {
            Debug.WriteLine("2");
        }
        
        // When we find an item that is in stock, alert the user
        public void InStock()
        {
            //Make Noise
            //Record somewhere
            //open link (only once)
            Debug.WriteLine("In Stock");
        }

        public void NotInStock()
        {
            Debug.WriteLine("Not in Stock");
        }

        // HtmlAgilityPack variable used by child scrapers to access the web.
        protected HtmlWeb web;
        // Notes which Website this scraper is for
        protected Website ws;

    }
}

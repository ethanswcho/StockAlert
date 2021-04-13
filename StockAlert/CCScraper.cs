using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            base.Scrape(URLDict);
        }
    }
}

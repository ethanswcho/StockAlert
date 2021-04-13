using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Specialized;
using HtmlAgilityPack;

/*
namespace StockAlert
{
    interface IWebScraper
    {

        public WebScraper(List<string> NVIDIAWanted, List<string> AMDWanted)
        {
            Debug.WriteLine("Selected options:");
            Debug.WriteLine("   NVIDIA:");
            foreach (string model in NVIDIAWanted)
            {
                Debug.WriteLine("       " + model);
            }
            Debug.WriteLine("\n   AMD:");
            foreach (string model in AMDWanted)
            {
                Debug.WriteLine("       " + model);
            }
            Debug.WriteLine("\n");

            this.initNVIDIALinks();
            this.initNVIDIAQuery(NVIDIAWanted);
            this.initAMDLinks();
            this.initAMDQuery(AMDWanted);
        }

        private void initNVIDIALinks()
        {

        }

        public void initNVIDIAQuery(List<string> wanted)
        {
            this.NVIDIAQuery = new OrderedDictionary();
            foreach (string name in wanted)
            {
                this.NVIDIAQuery.Add(name, this.NVIDIALinks[name]);
            }
        }

        private void initAMDLinks()
        {

        }

        private void initAMDQuery(List<string> wanted)
        {
            this.AMDQuery = new OrderedDictionary();
            foreach (string name in wanted)
            {
                this.AMDQuery.Add(name, this.AMDLinks[name]);
            }
        }

        private void ScrapePage(string URL)
        {

        }

        protected private HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
        protected private OrderedDictionary NVIDIAQuery;
        protected private OrderedDictionary AMDQuery;
        protected private Dictionary<string, string> NVIDIALinks;
        protected private Dictionary<string, string> AMDLinks;

    }
}
*/
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
    // Supported websites
    enum Website
    {
        MemoryExpress,
        CanadaComputers
    }

    // Supported GPU makers
    enum Maker
    {
        NVIDIA,
        AMD
    }


    // Builds queries of websites to scrape, then sorts them by their parent website

    class QueryBuilder
    {
        public QueryBuilder(List<string> NVIDIAWanted, List<string> AMDWanted)
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

            this.NVIDIAWanted = NVIDIAWanted;
            this.AMDWanted = AMDWanted;
            this.initURLDict();

            Debug.WriteLine("Loaded URLS:");
            this.PrettyPrintDict(this.URLDict);
        }

        // Wanted product numbers from each maker
        private List<string> NVIDIAWanted, AMDWanted;

        // Holds URL for each product from each website => maker
        private Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>> URLDict;

        private void initURLDict()
        {
            URLDict = new Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>>();

            // MemoryExpress
            URLDict[Website.MemoryExpress] = new Dictionary<Maker, Dictionary<string, string>>();
            // MemoryExpress - NVIDIA Links
            URLDict[Website.MemoryExpress][Maker.NVIDIA] = new Dictionary<string, string>()
            {                
                {"3090", "https://www.memoryexpress.com/Category/VideoCards?FilterID=0faf222f-0400-d211-b926-04fdfc0bfa85" },
                { "3080", "https://www.memoryexpress.com/Category/VideoCards?FilterID=45788ec3-6bb1-e460-abe6-afa274b9d30e" },
                {"3070", "https://www.memoryexpress.com/Category/VideoCards?FilterID=e3034e65-f2ac-35f1-26eb-277b7a7e9ce9" },
                {"3060TI", "https://www.memoryexpress.com/Category/VideoCards?FilterID=75668704-944f-8e1a-4cca-036beb9638a8" },
                {"3060", "https://www.memoryexpress.com/Category/VideoCards?FilterID=f1b0a6e4-f41e-5fea-c242-d1bac7b02bf2"},
                // Links for testing. Should return 1 in stock.
                {"Test1", "https://www.memoryexpress.com/Category/VideoCards?FilterID=d9eeb36d-9106-8db7-6cef-725efd164818"},
                {"Test2", "https://www.memoryexpress.com/Category/SingleBoardComputers" }
            };
            // MemoryExpress - AMD Links
            URLDict[Website.MemoryExpress][Maker.AMD] = new Dictionary<string, string>()
            {
                {"6900XT", "https://www.memoryexpress.com/Category/VideoCards?FilterID=a546466e-2a58-905e-129b-fc735319acbf" },
                {"6800XT", "https://www.memoryexpress.com/Category/VideoCards?FilterID=9705ada8-e2b2-0ac9-738e-5e92c99a5932" },
                {"6800", "https://www.memoryexpress.com/Category/VideoCards?FilterID=8d5ba2df-0447-8b14-4791-aee8db2800b0" },
                {"6700XT", "https://www.memoryexpress.com/Category/VideoCards?FilterID=0901d9d6-31e0-987f-382c-e66e7ee23a8a" },
                {"5700XT", "https://www.memoryexpress.com/Category/VideoCards?FilterID=0b1bde6f-293d-7718-bcce-dd2651933ddc" }
            };

            // CanadaComputers
            URLDict[Website.CanadaComputers] = new Dictionary<Maker, Dictionary<string, string>>();
            // CanadaComputers - NVIDIA Links
            URLDict[Website.CanadaComputers][Maker.NVIDIA] = new Dictionary<string, string>()
            {
                {"3090", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_3&mfr=&pr=" },
                {"3080", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_5&mfr=&pr=" },
                {"3070", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_7&mfr=&pr=" },
                {"3060TI", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_8&mfr=&pr=" },
                {"3060", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_9&mfr=&pr="},

                // Links for testing.
                {"Test1", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_27&mfr=&pr="},
                {"Test2", "https://www.canadacomputers.com/index.php?cPath=4&sf=:2_2&mfr=&pr="}
                
            };
            // CanadaComputers - AMD Links
            URLDict[Website.CanadaComputers][Maker.AMD] = new Dictionary<string, string>()
            {
                {"6900XT", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_29&mfr=&pr=" },
                {"6800XT", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_30&mfr=&pr=" },
                {"6800", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_31&mfr=&pr=" },
                {"6700XT", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_32&mfr=&pr=" },
                {"5700XT", "https://www.canadacomputers.com/index.php?cPath=43&sf=:3_33&mfr=&pr=" }
            };

        }

        // Matches the query selection (chosen by users from 1st page) and outputs the query in dictionary form
        public Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>> BuildAndGetQuery()
        {
            Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>> Query =
                new Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>>();

            // Get an iterable versino of all supported companies
            var Websites = Enum.GetValues(typeof(Website));

            foreach(Website w in Websites)
            {
                Query[w] = new Dictionary<Maker, Dictionary<string, string>>();
                Query[w][Maker.NVIDIA] = new Dictionary<string, string>();
                Query[w][Maker.AMD] = new Dictionary<string, string>();

                foreach (string model in NVIDIAWanted)
                {
                    // Find in URLDict the link given website, maker and model, and add it to the output query
                    Query[w][Maker.NVIDIA].Add(model, URLDict[w][Maker.NVIDIA][model]);
                }

                foreach(string model in AMDWanted)
                {
                    // Find in URLDict the link given website, maker and model, and add it to the output query
                    Query[w][Maker.AMD].Add(model, URLDict[w][Maker.AMD][model]);
                }
            }
            Debug.WriteLine("Filtered Query:");
            this.PrettyPrintDict(Query);
            return Query;
        }

        // Checks if the input (NVIDIAWanted and AMDWanted) is valid or not.
        // Currently only checks if they are null or not.


        // Helper method to pretty print nested dictionary structure for URLs and Query
        public void PrettyPrintDict(Dictionary<Website, Dictionary<Maker, Dictionary<string, string>>> dict)
        {
            foreach(var l1 in dict)
            {
                Debug.WriteLine(l1.Key.ToString());
                foreach(var l2 in l1.Value)
                {
                    Debug.WriteLine("   " + l2.Key.ToString());
                    foreach(var l3 in l2.Value)
                    {
                        Debug.WriteLine("       " + l3.Key + " => " + l3.Value);
                    }
                }
                Debug.WriteLine("");
            }
        }
    }
}


